using UBHG.Utility;
using UBHG.Utility.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace UBHG.UI.Buttons {
    [RequireComponent(typeof(Button))]
    public sealed class ContinueButton : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private SceneSwitcher sceneSwitcher;

        private LevelUtility.Level? lastPlayedLevel;

        private void Awake() => lastPlayedLevel = PlayerProgress.Load().LastPlayedLevel;

        private void Start() {
            var lastPlayedLevelExists = null != lastPlayedLevel;
            GetComponent<Button>().interactable = lastPlayedLevelExists;
            enabled = lastPlayedLevelExists;

            if (lastPlayedLevelExists) {
                return;
            }

            var hoverEffect = GetComponentInChildren<TextHoverEffect>();
            if (false == ReferenceEquals(null, hoverEffect)) {
                hoverEffect.enabled = false;
            }
        }

        public void LoadLastPlayedLevel() {
            if (null == lastPlayedLevel) {
                return;
            }

            sceneSwitcher.SwitchScenes(lastPlayedLevel?.SceneName);
        }
    }
}