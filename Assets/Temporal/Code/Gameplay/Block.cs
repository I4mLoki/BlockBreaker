using System;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Block : MonoBehaviour
    {
        private int hitsRemaining;

        private SpriteRenderer spriteRenderer;
        private TextMeshPro text;
        private GameplayGridSetup _gameplayGridSetup;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            text = GetComponentInChildren<TextMeshPro>();
            _gameplayGridSetup = FindObjectOfType<GameplayGridSetup>();
        }
        
        private void OnCollisionEnter2D()
        {
            hitsRemaining--;

            if (hitsRemaining > 0)
                UpdateVisualState();
            else
            {
                var block = gameObject.GetComponent<Block>();
                // _gameplayGridSetup.DestroyEnemy(block);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("VAR");
        }

        private void UpdateVisualState()
        {
            text.SetText(hitsRemaining.ToString());
            // spriteRenderer.color = Color.Lerp(Color.white, Color.red, hitsRemaining / 10f);
        }

        public void SetHits(int hits)
        {
            hitsRemaining = hits;
            UpdateVisualState();
        }

        public void Attack()
        {
        }
    }
}