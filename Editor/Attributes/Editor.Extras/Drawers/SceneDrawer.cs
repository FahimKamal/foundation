﻿using Pancake.Attribute;
using PancakeEditor.Attribute;
using UnityEditor;
using UnityEngine;

[assembly: RegisterAttributeDrawer(typeof(SceneDrawer), DrawerOrder.Decorator)]

namespace PancakeEditor.Attribute
{
    public class SceneDrawer : AttributeDrawer<SceneAttribute>
    {
        public override ExtensionInitializationResult Initialize(PropertyDefinition propertyDefinition)
        {
            var type = propertyDefinition.FieldType;
            if (type != typeof(string))
            {
                return "Scene attribute can only be used on field of type int or string";
            }

            return base.Initialize(propertyDefinition);
        }

        public override InspectorElement CreateElement(Property property, InspectorElement next) { return new SceneInspectorElement(property); }

        private class SceneInspectorElement : InspectorElement
        {
            private readonly Property _property;

            private SceneAsset _sceneAsset;

            public SceneInspectorElement(Property property) { _property = property; }

            protected override void OnAttachToPanel()
            {
                base.OnAttachToPanel();

                _property.ValueChanged += OnValueChanged;

                RefreshSceneAsset();
            }

            protected override void OnDetachFromPanel()
            {
                _property.ValueChanged -= OnValueChanged;

                base.OnDetachFromPanel();
            }

            public override float GetHeight(float width) { return EditorGUIUtility.singleLineHeight; }

            public override void OnGUI(Rect position)
            {
                EditorGUI.BeginChangeCheck();

                var asset = EditorGUI.ObjectField(position,
                    _property.DisplayName,
                    _sceneAsset,
                    typeof(SceneAsset),
                    false);

                if (EditorGUI.EndChangeCheck())
                {
                    var path = AssetDatabase.GetAssetPath(asset);
                    _property.SetValue(path);
                }
            }

            private void OnValueChanged(Property property) { RefreshSceneAsset(); }

            private void RefreshSceneAsset() { _sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(_property.Value as string); }
        }
    }
}