using System.Collections;
using UBHG.Common;
using UnityEngine;

namespace UBHG.Effects {
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Health))]
    public sealed class FlashEffect : MonoBehaviour {
        [Header("Parameters")]
        [SerializeField] private float duration;

        [Header("Material")]
        [SerializeField] private Material flashMaterial;

        private Material defaultMaterial;
        private Coroutine runningFlashRoutine;

        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultMaterial = spriteRenderer.material;
        }

        private void Start() {
            var health = GetComponent<Health>();
            health.Damaged += Flash;
        }

        private void Flash() {
            if (null != runningFlashRoutine) {
                StopCoroutine(runningFlashRoutine);
            }

            spriteRenderer.material = flashMaterial;

            IEnumerator ResetMaterial() {
                yield return new WaitForSeconds(duration);
                spriteRenderer.material = defaultMaterial;
            }

            runningFlashRoutine = StartCoroutine(ResetMaterial());
        }
    }
}