﻿namespace Pancake.Editor.Guide
{
    using UnityEngine;

    public class Conditionals_HideIfSample : ScriptableObject
    {
        public bool visible;

        [HideIf(nameof(visible))] public float val;
    }
}