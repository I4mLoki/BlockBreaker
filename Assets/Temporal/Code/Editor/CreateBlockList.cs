using Code.DataConfig.DataLists;
using UnityEditor;
using UnityEngine;
namespace Code.Editor
{
    public class CreateBlockList
    {
        [MenuItem("Assets/Create/Block List")]
        public static BaseBlockList Create()
        {
            var asset = ScriptableObject.CreateInstance<BaseBlockList>();

            AssetDatabase.CreateAsset(asset, "Assets/Data/BaseBlockList.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }
    }
}