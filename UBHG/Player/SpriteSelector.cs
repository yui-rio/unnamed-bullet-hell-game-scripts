using System;
using System.Collections.Generic;
using UBHG.Extensions;
using UnityEngine;

namespace UBHG.Player {
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Health))]
    public sealed class SpriteSelector : MonoBehaviour {
        [Header("Parameters")]
        [Tooltip("Sprite index corresponds to (current health - 1)")]
        [SerializeField] private List<Sprite> healthBasedSprites;

        private Health health;

        private SpriteRenderer spriteRenderer;

        private void Awake() => (spriteRenderer, health) = this.GetComponents<SpriteRenderer, Health>();

        private void Start() => health.Damaged += SelectSprite;

        private void SelectSprite() {
            var spriteIndex = Mathf.Clamp(health.Current - 1, 0, healthBasedSprites.Count - 1);
            spriteRenderer.sprite = healthBasedSprites[spriteIndex];
        }
    }
}