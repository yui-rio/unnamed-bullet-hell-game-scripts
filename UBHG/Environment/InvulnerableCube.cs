using UnityEngine;

namespace UBHG.Environment {
    [RequireComponent(typeof(Animator))]
    public sealed class InvulnerableCube : MonoBehaviour {
        private const float ANIMATION_DURATION = 0.5f;
        private static readonly int destroyTrigger = Animator.StringToHash("PhaseOut");

        private Animator animator;

        private bool isBeingDestroyed;

        private void Awake() => animator = GetComponent<Animator>();

        internal void Destroy() {
            if (isBeingDestroyed) {
                return;
            }

            isBeingDestroyed = true;

            animator.SetTrigger(destroyTrigger);
            Destroy(gameObject, ANIMATION_DURATION);
        }
    }
}