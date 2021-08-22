using System.Collections.Generic;
using TMPro;
using UBHG.Utility;
using UBHG.Utility.Levels;
using UnityEngine;

namespace UBHG.UI {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class LevelSelector : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] private SceneSwitcher sceneSwitcher;

        private int currentLevelIndex;

        private IList<LevelUtility.Level> levels;

        private TextMeshProUGUI textMesh;

        private LevelUtility.Level SelectedLevel => levels[currentLevelIndex];

        private void Awake() {
            textMesh = GetComponent<TextMeshProUGUI>();

            levels = PlayerProgress.Load().AvailableLevels;

            if (levels.Count > 0) {
                return;
            }

            Debug.LogError("No levels exist");
        }

        private void Start() => UpdateText();

        public void DisplayNextLevel() {
            if (0 == levels.Count) {
                return;
            }

            currentLevelIndex = (currentLevelIndex + 1) % levels.Count;
            UpdateText();
        }

        public void DisplayPreviousLevel() {
            if (0 == levels.Count) {
                return;
            }

            currentLevelIndex = (currentLevelIndex + levels.Count - 1) % levels.Count;
            UpdateText();
        }

        public void PlaySelectedLevel() => sceneSwitcher.SwitchScenes(SelectedLevel.SceneName);

        private void UpdateText() => textMesh.text = SelectedLevel.Name;
    }
}