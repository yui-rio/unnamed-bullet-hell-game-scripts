using UBHG.Player;
using UBHG.Utility;
using UBHG.Utility.Time;
using UnityEngine;

namespace UBHG.UI.GameUI {
    [RequireComponent(typeof(Animator))]
    public sealed class GameUI : MonoBehaviour {
        private static readonly int gameOverTrigger = Animator.StringToHash("GameOver");

        private static readonly int idleTrigger = Animator.StringToHash("Idle");

        private static readonly int pauseTrigger = Animator.StringToHash("Pause");

        [Header("Dependencies")]
        [SerializeField] private SceneSwitcher sceneSwitcher;

        [SerializeField] private TimeScaler timeScaler;

        private Animator animator;

        private bool gameOver;

        private bool CanTogglePause =>
            false == gameOver
            && false == sceneSwitcher.Switching
            && Input.GetKeyDown(KeyCode.Tab);

        private void Awake() => animator = GetComponent<Animator>();

        private void Start() {
            var player = PlayerCharacter.Instance;
            if (null != player) {
                player.Killed += OnPlayerKilled;
            }
        }

        private void LateUpdate() {
            if (false == CanTogglePause) {
                return;
            }

            if (timeScaler.GameRunning) {
                PauseGame();
            } else {
                UnpauseGame();
            }
        }

        private void PauseGame() {
            animator.SetTrigger(pauseTrigger);
            timeScaler.PauseGame();
        }

        private void UnpauseGame() {
            timeScaler.ResumeGame();
            animator.SetTrigger(idleTrigger);
        }

        private void OnPlayerKilled() {
            gameOver = true;
            TransitionToGameOverScreen();
        }

        private void TransitionToGameOverScreen() => animator.SetTrigger(gameOverTrigger);
    }
}