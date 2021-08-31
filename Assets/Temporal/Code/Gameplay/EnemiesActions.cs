using System;
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