using System.Collections.Generic;
using DataConfig;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Temporal.Code.Editor
{
    public class BaseBlockEditorWindow : OdinEditorWindow
    {
        private static void OpenWindow()
        {
            GetWindow<BaseBlockEditorWindow>().Show();
        }

        [PropertyOrder(-10)]
        [HorizontalGroup]
        [Button(ButtonSizes.Large)]
        public void SomeButton1() { }

        [HorizontalGroup]
        [Button(ButtonSizes.Large)]
        public void SomeButton2() { }

        [HorizontalGroup]
        [Button(ButtonSizes.Large)]
        public void SomeButton3() { }

        [HorizontalGroup]
        [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
        public void SomeButton4() { }

        [HorizontalGroup]
        [Button(ButtonSizes.Large), GUIColor(1, 0.5f, 0)]
        public void SomeButton5() { }

        [TableList]
        public List<BaseBlockList> SomeTableData;
    }
}
