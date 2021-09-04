using System;
using Code.DataConfig.BaseObjects;
using DataConfig;
using UnityEngine;
namespace Gameplay
{
    public class EnemiesActions : MonoBehaviour
    {
        private EnemiesTurn _enemiesTurn;

        public void LoadComponents()
        {
            _enemiesTurn = GetComponent<EnemiesTurn>();
        }

        public void InitialEnemiesTurn(BaseLevel level)
        {
            Debug.Log("Enemies turn");
            StartCoroutine(_enemiesTurn.InitialLoad(level, OnEnemiesTurnEnd));
        }

        public void EnemiesTurn()
        {
            StartCoroutine(_enemiesTurn.StartTurn(OnEnemiesTurnEnd));
        }

        public void OnEnemiesTurnEnd()
        {
            GameplayManager.Instance.SetPlayerTurn();
        }
    }
}