using System;
using Code.DataConfig.BaseObjects;
using DataConfig;
using UnityEngine;
namespace Gameplay
{
    public class EnemiesActions : MonoBehaviour
    {
        private GameplayGridSetup _gridSetup;

        public void LoadComponents()
        {
            _gridSetup = GetComponent<GameplayGridSetup>();
        }

        public void InitialEnemiesTurn(BaseLevel level)
        {
            Debug.Log("Enemies turn");
            StartCoroutine(_gridSetup.InitialLoad(level, OnEnemiesTurnEnd));
        }

        public void EnemiesTurn()
        {
            StartCoroutine(_gridSetup.StartTurn(OnEnemiesTurnEnd));
        }

        public void OnEnemiesTurnEnd()
        {
            GameplayManager.Instance.SetPlayerTurn();
        }
    }
}