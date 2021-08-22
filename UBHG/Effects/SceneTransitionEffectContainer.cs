using System.Collections.Generic;
using UnityEngine;

namespace UBHG.Effects {
    public sealed class SceneTransitionEffectContainer : MonoBehaviour {
        private const float DEFAULT_DELAY = 1f;

        private static readonly int transitionTrigger = Animator.StringToHash("Fade");

        [Header("Parameters")]
        [Tooltip("Duration of transition effect phase, i.e. duration of fade-out or fade-in.")]
        [SerializeField] private float transitionPhaseDuration = DEFAULT_DELAY;

        [Header("Managed objects")]
        [SerializeField] private List<Animator> animators;

        internal float TransitionPhaseDuration => transitionPhaseDuration;

        internal void StartTransition() =>
            animators.ForEach(animator => {
                if (ReferenceEquals(null, animator.runtimeAnimatorController)) {
                    return;
                }

                // set updateMode to Unscaled Time for transitions from pause menu
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;
                animator.SetTrigger(transitionTrigger);
            });
    }
}