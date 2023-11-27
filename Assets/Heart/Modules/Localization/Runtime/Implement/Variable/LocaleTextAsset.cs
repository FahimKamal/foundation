﻿using System;
using System.Linq;
using UnityEngine;

namespace Pancake.Localization
{
    [CreateAssetMenu(menuName = "Pancake/Localization/TextAsset", order = 5)]
    [EditorIcon("scriptable_yellow_textasset")]
    public class LocaleTextAsset : LocaleVariable<TextAsset>
    {
        [Serializable]
        private class TextAssetLocaleItem : LocaleItem<TextAsset>
        {
        };

        [SerializeField] private TextAssetLocaleItem[] items = new TextAssetLocaleItem[1];
        public override LocaleItemBase[] LocaleItems => items.ToArray<LocaleItemBase>();
    }
}