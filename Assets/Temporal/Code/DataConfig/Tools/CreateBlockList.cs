using UnityEditor;
using UnityEngine;

namespace DataConfig
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