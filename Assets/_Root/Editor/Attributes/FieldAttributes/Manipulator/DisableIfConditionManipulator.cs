﻿using UnityEditor;

namespace Pancake.Editor
{
    [ManipulatorTarget(typeof(DisableIfAttribute))]
    sealed class DisableIfConditionManipulator : ConditionManipulator
    {
        /// <summary>
        /// Called before rendering member GUI.
        /// </summary>
        public override void OnBeforeGUI() { EditorGUI.BeginDisabledGroup(EvaluateExpression()); }

        /// <summary>
        /// Called after rendering member GUI.
        /// </summary>
        public override void OnAfterGUI() { EditorGUI.EndDisabledGroup(); }
    }
}