// ItemDropdownAttribute.cs
using System;
using System.Linq;
using DataConfig;
using Sirenix.OdinInspector.Editor;
using Temporal.Code.DataConfig.BaseObjects;
using UnityEditor;
using UnityEngine;
namespace Temporal.Code.DataConfig.Tools
{
    public class BaseBlockDropdownAttribute : Attribute { }

    public class ItemDropdownAttributeDrawer<T> : OdinAttributeDrawer<BaseBlockDropdownAttribute, T> where T : Progress.Item
    {
        private GenericSelector<BaseBlock> _selector;

        protected override void Initialize()
        {
            // Get your item types here via your AssetUtil function
            var blocks = AssetDatabase
                .FindAssets("t:BaseBlock")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<BaseBlock>)
                .ToList();

            _selector = new GenericSelector<BaseBlock>("BaseBlock", false, item => item.blockName, blocks);
            _selector.EnableSingleClickToSelect();
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var currentBlock = ValueEntry.WeakSmartValue as BaseBlock;
            var buttonLabel = currentBlock?.blockName ?? "Pick An Block";
            var rect = EditorGUILayout.GetControlRect(label != null, 42f);

            var selectedValues = OdinSelector<BaseBlock>.DrawSelectorDropdown(rect, buttonLabel, rect1 =>
            {
                _selector.ShowInPopup();
                return _selector;
            });

            ValueEntry.WeakSmartValue = selectedValues?.First() ?? currentBlock;
        }
    }
}