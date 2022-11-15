﻿using System;
using System.Diagnostics;

namespace Pancake
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class DeclareFoldoutGroupAttribute : DeclareGroupBaseAttribute
    {
        public DeclareFoldoutGroupAttribute(string path)
            : base(path)
        {
        }

        public string Title { get; set; }
        public bool Expanded { get; set; }
    }
}