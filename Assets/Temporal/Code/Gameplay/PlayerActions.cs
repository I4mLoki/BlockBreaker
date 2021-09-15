using UnityEngine;
namespace Gameplay
{
    public class PlayerActions : MonoBehaviour
    {
        private BulletShooter _bulletShooter;

        public void LoadComponents()
        {
            _bulletShooter = FindObjectOfType<BulletShooter>();
        }

        public void PlayerTurn()
        {
            _bulletShooter.StartBulletShooting(OnPlayerTurnEnd);
        }

        public void OnPlayerTurnEnd()
        {
            GameplayManager.Instance.SetEnemiesTurn();
        }
    }
}