using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UBHG.LevelProgress {
    public abstract class LevelActivity : MonoBehaviour {
        [SerializeField] private List<LevelActivity> next;

        private int previousStagesToComplete;

        protected void Awake() {
            foreach (var levelActivity in next) {
                levelActivity.AddActivityDependency();
            }
        }

        protected void Start() {
            if (0 == previousStagesToComplete) {
                StartActivity();
            }
        }

        private void StartActivity() {
            IEnumerator ActivityRoutine() {
                // Avoid executing multiple actions in one frame (hang danger) 
                yield return null;
                yield return ActivityAction();
                ActivityCompleted();
            }

            StartCoroutine(ActivityRoutine());
        }

        protected abstract IEnumerator ActivityAction();

        private void ActivityCompleted() {
            foreach (var levelActivity in next) {
                levelActivity.PreviousActivityCompleted();
            }
        }

        private void AddActivityDependency() => previousStagesToComplete += 1;

        private void PreviousActivityCompleted() {
            previousStagesToComplete -= 1;

            if (0 == previousStagesToComplete) {
                StartActivity();
            }
        }
    }
}