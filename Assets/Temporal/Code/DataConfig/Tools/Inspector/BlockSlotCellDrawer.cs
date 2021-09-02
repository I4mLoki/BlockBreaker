using Code.DataConfig.BaseObjects;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace Code.DataConfig.Tools.Inspector
{

    internal sealed class BlockSlotCellDrawer<TArray> : TwoDimensionalArrayDrawer<TArray, BaseBlockProperties>
        where TArray : System.Collections.IList
    {
        protected override TableMatrixAttribute GetDefaultTableMatrixAttributeSettings()
        {
            return new TableMatrixAttribute(){
                SquareCells = true, HideColumnIndices = true, HideRowIndices = true, ResizableColumns = false
            };
        }

        protected override BaseBlockProperties DrawElement(Rect rect, BaseBlockProperties value)
        {
            var id = DragAndDropUtilities.GetDragAndDropId(rect);
            var labelRect = new Rect(rect.x + 1f, rect.y + 1f, rect.width, 20f);

            DragAndDropUtilities.DrawDropZone(rect, value.block ? value.block.blockIcon : null, null, id); // Draws the drop-zone using the items icon.

            rect.y += 21f;
            rect.height -= 20f;

            if (value.block != null)
            {
                value.hits = EditorGUI.IntField(labelRect, value.hits, EditorStyles.toolbarButton);
            }

            value = DragAndDropUtilities.DropZone(rect, value);
            value.block = DragAndDropUtilities.DropZone<BaseBlock>(rect, value.block);
            value.block = DragAndDropUtilities.ObjectPickerZone(rect, value.block, false, id);

            return value;
        }
    }
}
#endif