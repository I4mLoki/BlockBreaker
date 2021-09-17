using Code.DataConfig.Characters;
using Code.DataConfig.DataLists;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace Code.Editor
{
    public class CreateCharacterData
    {
        private DataList _dataList;

        [InlineEditor(Expanded = true)]
        public BaseCharacter characterData;

        public CreateCharacterData(DataList dataList)
        {
            characterData = ScriptableObject.CreateInstance<BaseCharacter>();
            characterData.name = "New Character";
            _dataList = dataList;
        }

        [Button("Add New Character")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(characterData, _dataList.dataPath.characterPath + "/Character - " + (_dataList.baseCharacterList.list.Count + 1) + " - " + characterData.name+ ".asset");
            AssetDatabase.SaveAssets();
            _dataList.baseCharacterList.list.Add(characterData);
        }
    }
}