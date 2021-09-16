using System;
using Code.DataConfig.DataLists;
using UnityEngine;
namespace Code.Core
{
    public class CharactersTab : MonoBehaviour
    {
        [SerializeField] private DataList dataList;
        [SerializeField] private Transform unlockedCharacters;
        [SerializeField] private Transform lockedCharacters;

        private void LoadCharacters()
        {

        }
    }
}