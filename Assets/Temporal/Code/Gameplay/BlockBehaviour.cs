﻿using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class BlockBehaviour : MonoBehaviour
    {
        private int hitsRemaining = 5;

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
                Destroy(gameObject);
        }

        private void UpdateVisualState()
        {
            text.SetText(hitsRemaining.ToString());
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, hitsRemaining / 10f);
        }

        public void SetHits(int hits)
        {
            hitsRemaining = hits;
            UpdateVisualState();
        }
    }
}