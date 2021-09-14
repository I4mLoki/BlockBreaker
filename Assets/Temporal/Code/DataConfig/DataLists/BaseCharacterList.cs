using System.Collections.Generic;
using Code.DataConfig.Characters;
using UnityEngine;
namespace Code.DataConfig.DataLists
{
    [CreateAssetMenu(menuName = "BlockBreaker/Data/Create BaseCharacterList", fileName = "BaseCharacterList", order = 0)]
    public class BaseCharacterList : ScriptableObject
    {
        [ColorBox]
        public List<BaseCharacter> list;
    }
}