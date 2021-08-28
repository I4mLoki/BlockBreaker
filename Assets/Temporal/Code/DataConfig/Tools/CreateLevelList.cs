using UnityEditor;
using UnityEngine;

namespace DataConfig
{
    public class CreateLevelList
    {
        [MenuItem("Assets/Create/Level List")]
        public static BaseLevelList Create()
        {
            var asset = ScriptableObject.CreateInstance<BaseLevelList>();

            AssetDatabase.CreateAsset(asset, "Assets/Data/BaseLevelList.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }
    }
}