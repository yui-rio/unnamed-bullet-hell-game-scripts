using System.Collections;
using UBHG.Effects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UBHG.Utility {
    public sealed class SceneSwitcher : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] private SceneTransitionEffectContainer effectContainer;

        // BUG: cannot switch scenes during fade-in
        internal bool Switching { get; private set; } = true;

        private void Start() {
            IEnumerator EndSwitchingLater() {
                yield return new WaitForSeconds(effectContainer.TransitionPhaseDuration);
                Switching = false;
            }

            StartCoroutine(EndSwitchingLater());
        }

        public void SwitchScenes(string sceneName) {
            if (Switching) {
                return;
            }

            Switching = true;

            effectContainer.StartTransition();

            IEnumerator SwitchLater() {
                yield return new WaitForSecondsRealtime(effectContainer.TransitionPhaseDuration);
                SceneManager.LoadScene(sceneName);
            }

            StartCoroutine(SwitchLater());
        }

        internal void LoadNextScene() {
            if (Switching) {
                return;
            }

            Switching = true;

            var nextSceneBuildIndex =
                (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;

            effectContainer.StartTransition();

            IEnumerator SwitchLater() {
                yield return new WaitForSecondsRealtime(effectContainer.TransitionPhaseDuration);
                SceneManager.LoadScene(nextSceneBuildIndex);
            }

            StartCoroutine(SwitchLater());
        }

        internal void RestartScene() => SwitchScenes(SceneManager.GetActiveScene().name);
    }
}