using DataConfig;
#if UNITY_EDITOR
namespace Sirenix.OdinInspector.Demos.RPGEditor
{
    using UnityEngine;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.OdinInspector.Editor.Drawers;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;
    using UnityEditor;

    //
    // In Character.cs we have a two dimention array of ItemSlots which is our inventory.
    // And instead of using the the TableMatrix attribute to customize it there, we in this case
    // instead create a custom drawer that will work for all two-dimentional ItemSlot arrays,
    // so we don't have to make the same CustomDrawer via the TableMatrix attribute again and again.
    //

    internal sealed class ItemSlotCellDrawer<TArray> : TwoDimensionalArrayDrawer<TArray, BaseBlock>
        where TArray : System.Collections.IList
    {
        protected override TableMatrixAttribute GetDefaultTableMatrixAttributeSettings()
        {
            return new TableMatrixAttribute()
            {
                SquareCells = true,
                HideColumnIndices = false,
                HideRowIndices = false,
                ResizableColumns = false,
                RespectIndentLevel = true
            };
        }

        [ValueDropdown("GetBaseBlockList", AppendNextDrawer = true)]
        protected override BaseBlock DrawElement(Rect rect, BaseBlock value)
        {
            // if (value.sizeX > 1)
            //     rect.SetSize(rect.width + value.sizeX, rect.height);
            // if (value.sizeY > 1)
            //     rect.SetSize(rect.width, rect.height + value.sizeY);

            var id = DragAndDropUtilities.GetDragAndDropId(rect);
            DragAndDropUtilities.DrawDropZone(rect, value ? value.blockIcon : null, null, id); // Draws the drop-zone using the items icon.
            GUILayout.Space(5);
            if (value != null)
            {
                // Item count
                // var countRect = rect.Padding(5).AlignBottom(16);
                // value.blockProperties.hits = EditorGUI.IntField(countRect, value.blockProperties.hits);
                // GUI.Label(countRect, "/ " + value.blockProperties.hits, SirenixGUIStyles.RightAlignedGreyMiniLabel);
            }

            value = DragAndDropUtilities.DropZone(rect, value);
            value = DragAndDropUtilities.DropZone<BaseBlock>(rect, value);
            value = DragAndDropUtilities.DragZone(rect, value, true, true);
            value = DragAndDropUtilities.ObjectPickerZone(rect, value, false, id);

            return value;
        }

        // protected override void DrawPropertyLayout(GUIContent label)
        // {
            // base.DrawPropertyLayout(label);
            //
            // // Draws a drop-zone where we can destroy items.
            // var rect = GUILayoutUtility.GetRect(50, 40).Padding(1);
            // var id = DragAndDropUtilities.GetDragAndDropId(rect);
            // DragAndDropUtilities.DrawDropZone(rect, null as UnityEngine.Object, null, id);
            // DragAndDropUtilities.DropZone<BaseBlock>(rect, new BaseBlock(), false, id);
        // }
    }

}
#endif
