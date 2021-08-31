using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Block : MonoBehaviour
    {
        private int hitsRemaining;

        private SpriteRenderer spriteRenderer;
        private TextMeshPro text;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            text = GetComponentInChildren<TextMeshPro>();
        }
        
        private void OnCollisionEnter2D()
        {
            hitsRemaining--;

            if (hitsRemaining > 0)
                UpdateVisualState();
            else
            {
                gameObject.SetActive(false);
                // GameplayManager.Instance.blockList.Remove(this);
                Destroy(gameObject, .5f);
            }
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
    }
}