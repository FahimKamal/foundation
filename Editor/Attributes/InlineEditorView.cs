﻿using UnityEditor;
using UnityEngine;

namespace Pancake.Editor
{
    [ViewTarget(typeof(InlineEditorAttribute))]
    sealed class InlineEditorView : FieldView, ITypeValidationCallback
    {
        /// <summary>
        /// Called for drawing element view GUI.
        /// </summary>
        /// <param name="position">Position of the serialized element.</param>
        /// <param name="element">Serialized element with ViewAttribute.</param>
        /// <param name="label">Label of serialized element.</param>
        public override void OnGUI(Rect position, SerializedField element, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            element.DrawChildren(position);
            if (EditorGUI.EndChangeCheck())
            {
                element.ApplyNestedProperties();
            }
        }

        /// <summary>
        /// Get height which needed to draw property.
        /// </summary>
        /// <param name="element">Serialized element with ViewAttribute.</param>
        /// <param name="label">Label of serialized element.</param>
        public override float GetHeight(SerializedField element, GUIContent label) { return element.GetChildrenHeight(); }

        /// <summary>
        /// Return true if this property valid the using with this attribute.
        /// If return false, this property attribute will be ignored.
        /// </summary>
        /// <param name="property">Reference of serialized property.</param>
        public bool IsValidProperty(SerializedProperty property) { return property.hasVisibleChildren; }
    }
}