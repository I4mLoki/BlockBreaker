using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.OdinInspector.Editor.Examples;

public class TablesMatrixExamples : MonoBehaviour
{
    [TableMatrix(HorizontalTitle = "Square Celled Matrix", SquareCells = true)]
    public Texture2D[,] SquareCelledMatrix;

    [TableMatrix(SquareCells = true)]
    public Mesh[,] PrefabMatrix;

    [OnInspectorInit]
    private void CreateData() {
        SquareCelledMatrix = new Texture2D[8, 4]
        {
            { ExampleHelper.GetTexture(), null, null, null },
            { null, ExampleHelper.GetTexture(), null, null },
            { null, null, ExampleHelper.GetTexture(), null },
            { null, null, null, ExampleHelper.GetTexture() },
            { ExampleHelper.GetTexture(), null, null, null },
            { null, ExampleHelper.GetTexture(), null, null },
            { null, null, ExampleHelper.GetTexture(), null },
            { null, null, null, ExampleHelper.GetTexture() },
        };

        PrefabMatrix = new Mesh[8, 4]
        {
            { ExampleHelper.GetMesh(), null, null, null },
            { null, ExampleHelper.GetMesh(), null, null },
            { null, null, ExampleHelper.GetMesh(), null },
            { null, null, null, ExampleHelper.GetMesh() },
            { null, null, null, ExampleHelper.GetMesh() },
            { null, null, ExampleHelper.GetMesh(), null },
            { null, ExampleHelper.GetMesh(), null, null },
            { ExampleHelper.GetMesh(), null, null, null },
        };
    }
}