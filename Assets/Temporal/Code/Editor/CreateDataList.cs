using Code.DataConfig.DataLists;
using UnityEditor;
using UnityEngine;
namespace Code.Editor
{
    public class CreateDataList
    {
        [MenuItem("Assets/Create/Data List")]
        public static DataList Create()
        {
            var asset = ScriptableObject.CreateInstance<DataList>();

            AssetDatabase.CreateAsset(asset, "Assets/Data/DataList.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }
    }
}