using Code.DataConfig.DataLists;
using UnityEditor;
using UnityEngine;
namespace Code.Editor
{
    public class CreateBoosterList
    {
        [MenuItem("Assets/Create/Booster List")]
        public static BaseBoosterList Create()
        {
            var asset = ScriptableObject.CreateInstance<BaseBoosterList>();

            AssetDatabase.CreateAsset(asset, "Assets/Data/BaseBoosterList.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }
    }
}