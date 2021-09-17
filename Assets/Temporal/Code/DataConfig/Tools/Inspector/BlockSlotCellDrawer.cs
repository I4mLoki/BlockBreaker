using Code.DataConfig.BaseObjects;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities;
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

        [OnInspectorGUI]
        protected override BaseBlockProperties DrawElement(Rect rect, BaseBlockProperties value)
        {
            var id = DragAndDropUtilities.GetDragAndDropId(rect);
            var labelRect = new Rect(rect.x + 5f, rect.y - 5f, rect.width - 10f, 20f);
            var buttonRect = new Rect(rect.x, rect.y + 58f, rect.width - 58f, 16f);

            if (value.block != null)
            {
                var blockRect = new Rect(rect.x, rect.y, rect.width*value.block.sizeX, rect.height*value.block.sizeY);
                DragAndDropUtilities.DrawDropZone(blockRect, value.block ? value.block.blockIcon : null, null, id);
                DragAndDropUtilities.DisallowedDropAreaForNextDragZone(blockRect);
                value.hits = EditorGUI.IntField(labelRect, value.hits, EditorStyles.toolbarTextField);
                if (GUI.Button(buttonRect, "X"))
                    value.block = null;
            }

            value = DragAndDropUtilities.DropZone(rect, value);
            value.block = DragAndDropUtilities.DropZone<BaseBlock>(rect, value.block);

            value.block = DragAndDropUtilities.DragZone(rect, value.block, true, false);

            return value;
        }
    }
}
#endif