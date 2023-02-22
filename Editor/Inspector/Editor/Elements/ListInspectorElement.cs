﻿using System;
using System.Collections;
using InspectorUnityInternalBridge;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Pancake.Editor
{
    public class ListInspectorElement : InspectorElement
    {
        private const float ListExtraWidth = 7f;
        private const float DraggableAreaExtraWidth = 14f;

        private readonly Property _property;
        private readonly ReorderableList _reorderableListGui;
        private readonly bool _alwaysExpanded;

        private float _lastContentWidth;

        protected ReorderableList ListGui => _reorderableListGui;

        public ListInspectorElement(Property property)
        {
            property.TryGetAttribute(out ListDrawerSettingsAttribute settings);

            _property = property;
            _alwaysExpanded = settings?.AlwaysExpanded ?? false;
            _reorderableListGui = new ReorderableList(null, _property.ArrayElementType)
            {
                draggable = settings?.Draggable ?? true,
                displayAdd = settings == null || !settings.HideAddButton,
                displayRemove = settings == null || !settings.HideRemoveButton,
                drawHeaderCallback = DrawHeaderCallback,
                elementHeightCallback = ElementHeightCallback,
                drawElementCallback = DrawElementCallback,
                onAddCallback = AddElementCallback,
                onRemoveCallback = RemoveElementCallback,
                onReorderCallbackWithDetails = ReorderCallback,
                multiSelect = settings != null && settings.MultiSelect
            };

            if (!_reorderableListGui.displayAdd && !_reorderableListGui.displayRemove)
            {
                _reorderableListGui.footerHeight = 0f;
            }
        }

        public override bool Update()
        {
            var dirty = false;

            if (_property.TryGetSerializedProperty(out var serializedProperty) && serializedProperty.isArray)
            {
                _reorderableListGui.serializedProperty = serializedProperty;
            }
            else if (_property.Value != null)
            {
                _reorderableListGui.list = (IList) _property.Value;
            }
            else if (_reorderableListGui.list == null)
            {
                _reorderableListGui.list = (IList) (_property.FieldType.IsArray
                    ? Array.CreateInstance(_property.ArrayElementType, 0)
                    : Activator.CreateInstance(_property.FieldType));
            }

            if (_alwaysExpanded && !_property.IsExpanded)
            {
                _property.IsExpanded = true;
            }

            if (_property.IsExpanded)
            {
                dirty |= GenerateChildren();
            }
            else
            {
                dirty |= ClearChildren();
            }

            dirty |= base.Update();

            if (dirty)
            {
                ReorderableListProxy.ClearCacheRecursive(_reorderableListGui);
            }

            return dirty;
        }

        public override float GetHeight(float width)
        {
            if (!_property.IsExpanded)
            {
                return _reorderableListGui.headerHeight + 4f;
            }

            _lastContentWidth = width;

            return _reorderableListGui.GetHeight();
        }

        public override void OnGUI(Rect position)
        {
            if (!_property.IsExpanded)
            {
                ReorderableListProxy.DoListHeader(_reorderableListGui, new Rect(position) {yMax = position.yMax - 4,});
                return;
            }

            var labelWidthExtra = ListExtraWidth + DraggableAreaExtraWidth;

            using (GuiHelper.PushLabelWidth(EditorGUIUtility.labelWidth - labelWidthExtra))
            {
                _reorderableListGui.DoList(position);
            }
        }

        private void AddElementCallback(ReorderableList reorderableList)
        {
            if (_property.TryGetSerializedProperty(out _))
            {
                ReorderableListProxy.defaultBehaviours.DoAddButton(reorderableList);
                _property.NotifyValueChanged();
                return;
            }

            var template = CloneValue(_property);

            _property.SetValues(targetIndex =>
            {
                var value = (IList) _property.GetValue(targetIndex);

                if (_property.FieldType.IsArray)
                {
                    var array = Array.CreateInstance(_property.ArrayElementType, template.Length + 1);
                    Array.Copy(template, array, template.Length);
                    value = array;
                }
                else
                {
                    if (value == null)
                    {
                        value = (IList) Activator.CreateInstance(_property.FieldType);
                    }

                    var newElement = CreateDefaultElementValue(_property);
                    value.Add(newElement);
                }

                return value;
            });
        }

        private void RemoveElementCallback(ReorderableList reorderableList)
        {
            if (_property.TryGetSerializedProperty(out _))
            {
                ReorderableListProxy.defaultBehaviours.DoRemoveButton(reorderableList);
                _property.NotifyValueChanged();
                return;
            }

            var template = CloneValue(_property);
            var ind = reorderableList.index;

            _property.SetValues(targetIndex =>
            {
                var value = (IList) _property.GetValue(targetIndex);

                if (_property.FieldType.IsArray)
                {
                    var array = Array.CreateInstance(_property.ArrayElementType, template.Length - 1);
                    Array.Copy(template,
                        0,
                        array,
                        0,
                        ind);
                    Array.Copy(template,
                        ind + 1,
                        array,
                        ind,
                        array.Length - ind);
                    value = array;
                }
                else
                {
                    value?.RemoveAt(ind);
                }

                return value;
            });
        }

        private void ReorderCallback(ReorderableList list, int oldIndex, int newIndex)
        {
            if (_property.TryGetSerializedProperty(out _))
            {
                _property.NotifyValueChanged();
                return;
            }

            var mainValue = _property.Value;

            _property.SetValues(targetIndex =>
            {
                var value = (IList) _property.GetValue(targetIndex);

                if (value == mainValue)
                {
                    return value;
                }

                var element = value[oldIndex];
                for (var index = 0; index < value.Count - 1; ++index)
                {
                    if (index >= oldIndex)
                    {
                        value[index] = value[index + 1];
                    }
                }

                for (var index = value.Count - 1; index > 0; --index)
                {
                    if (index > newIndex)
                    {
                        value[index] = value[index - 1];
                    }
                }

                value[newIndex] = element;

                return value;
            });
        }

        private bool GenerateChildren()
        {
            var count = _reorderableListGui.count;

            if (ChildrenCount == count)
            {
                return false;
            }

            while (ChildrenCount < count)
            {
                var property = _property.ArrayElementProperties[ChildrenCount];
                AddChild(CreateItemElement(property));
            }

            while (ChildrenCount > count)
            {
                RemoveChildAt(ChildrenCount - 1);
            }

            return true;
        }

        private bool ClearChildren()
        {
            if (ChildrenCount == 0)
            {
                return false;
            }

            RemoveAllChildren();

            return true;
        }

        protected virtual InspectorElement CreateItemElement(Property property)
        {
            return new PropertyInspectorElement(property, new PropertyInspectorElement.Props {forceInline = true,});
        }

        private void DrawHeaderCallback(Rect rect)
        {
            var labelRect = new Rect(rect);
            var arraySizeRect = new Rect(rect) {xMin = rect.xMax - 100,};

            if (_alwaysExpanded)
            {
                EditorGUI.LabelField(labelRect, _property.DisplayNameContent);
            }
            else
            {
                labelRect.x += 10;
                Uniform.Foldout(labelRect, _property);
            }

            var label = _reorderableListGui.count == 0 ? "Empty" : $"{_reorderableListGui.count} items";
            GUI.Label(arraySizeRect, label, Styles.ItemsCount);
            var previousColor = GUI.color;
            GUI.color = new Color(1f, 1f, 1f, 0f);
            GUI.Box(rect, "", new GUIStyle());
            GUI.color = previousColor;
            var @event = Event.current;
            switch (@event.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (rect.Contains(@event.mousePosition))
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                        if (@event.type == EventType.DragPerform)
                        {
                            DragAndDrop.AcceptDrag();

                            foreach (var objectDrop in DragAndDrop.objectReferences)
                            {
                                var objDropType = objectDrop.GetType();
                                if (objDropType == _property.ArrayElementType || (objDropType == typeof(GameObject) && (_property.ArrayElementType == typeof(Transform) || _property.ArrayElementType.BaseType == typeof(MonoBehaviour))))
                                {
                                    AddElementCallback(_reorderableListGui);

                                    if (_property.TryGetSerializedProperty(out var x))
                                    {
                                        x.GetArrayElementAtIndex(x.arraySize - 1).objectReferenceValue = objectDrop;
                                        _property.NotifyValueChanged();
                                    }
                                }
                            }
                        }
                    }

                    break;
            }
        }

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (index >= ChildrenCount)
            {
                return;
            }

            if (!_reorderableListGui.draggable)
            {
                rect.xMin += DraggableAreaExtraWidth;
            }

            var r = new Rect(rect);
            r.position -= new Vector2(18, 0);
            var alignRect = new Rect(r);
            if (index % 2 != 0 && !isFocused)
            {
                r.size += new Vector2(25, 0);
                r.position -= new Vector2(2, 0);
                Uniform.DrawBox(r, Uniform.ContentListDark);
            }
            else if (!isFocused)
            {
                r.size += new Vector2(25, 0);
                r.position -= new Vector2(2, 0);
                Uniform.DrawBox(r, Uniform.ContentList);
            }
            else
            {
                r.size += new Vector2(25, 0);
                r.position -= new Vector2(2, 0);
                Uniform.DrawBox(r, Uniform.ContentListBlue);
            }

            var previousColor = GUI.contentColor;
            GUI.contentColor = new Color(0.55f, 0.55f, 0.55f);
            EditorGUI.LabelField(alignRect, Uniform.IconContent("align_vertically_center", ""));
            GUI.contentColor = previousColor;
            GetChild(index).OnGUI(rect);
        }

        private float ElementHeightCallback(int index)
        {
            if (index >= ChildrenCount)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            return GetChild(index).GetHeight(_lastContentWidth);
        }

        private static object CreateDefaultElementValue(Property property)
        {
            var canActivate = property.ArrayElementType.IsValueType || property.ArrayElementType.GetConstructor(Type.EmptyTypes) != null;

            return canActivate ? Activator.CreateInstance(property.ArrayElementType) : null;
        }

        private static Array CloneValue(Property property)
        {
            var list = (IList) property.Value;
            var template = Array.CreateInstance(property.ArrayElementType, list?.Count ?? 0);
            list?.CopyTo(template, 0);
            return template;
        }

        private static class Styles
        {
            public static readonly GUIStyle ItemsCount;

            static Styles()
            {
                ItemsCount = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleRight,
                    normal = {textColor = EditorGUIUtility.isProSkin ? new Color(0.6f, 0.6f, 0.6f) : new Color(0.3f, 0.3f, 0.3f),},
                };
            }
        }
    }
}