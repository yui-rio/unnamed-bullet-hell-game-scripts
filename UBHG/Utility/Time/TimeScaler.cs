using System;
using System.Collections;
using UnityEngine;

namespace UBHG.Utility.Time {
    public sealed class TimeScaler : MonoBehaviour {
        private const float STOP_TIME = 0f;

        private const float DEFAULT_SLOW_SCALE = 0.25f;
        private const float DEFAULT_SLOW_DURATION = 1.25f;

        [Header("Parameters")]
        [Tooltip("Used when restoring Time.timeScale back to normal")]
        [SerializeField] private AnimationCurve speedUpCurve;

        private float normalFixedDeltaTime;

        private float normalTimeScale;

        private float savedTimeScale;

        private Coroutine slowRoutine;

        public bool GamePaused { get; private set; }

        internal bool GameRunning => !GamePaused;

        private void Awake() => (normalTimeScale, normalFixedDeltaTime) =
            (UnityEngine.Time.timeScale, UnityEngine.Time.fixedDeltaTime);

        private void OnDestroy() {
            if (null != slowRoutine) {
                StopCoroutine(slowRoutine);
            }

            ResetTimeScale();
        }

        internal void PauseGame() {
            if (GamePaused) {
                return;
            }

            GamePaused = true;
            savedTimeScale = UnityEngine.Time.timeScale;
            UnityEngine.Time.timeScale = STOP_TIME;
        }

        public void ResumeGame() {
            if (false == GamePaused) {
                return;
            }

            GamePaused = false;
            UnityEngine.Time.timeScale = savedTimeScale;
        }

        public void SlowTime() => SlowTime(DEFAULT_SLOW_SCALE, DEFAULT_SLOW_DURATION);

        public void SlowTime(float scale, float duration) {
            if (scale <= 0 || scale >= 1) {
                throw new ArgumentException(nameof(scale));
            }

            if (duration <= 0) {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            if (GamePaused) {
                Debug.LogWarning($"{nameof(SlowTime)} is called when {nameof(GamePaused)} is {GamePaused}");
                return;
            }

            if (null != slowRoutine) {
                StopCoroutine(slowRoutine);
            }

            slowRoutine = StartCoroutine(SlowRoutine(scale, duration));
        }

        private IEnumerator SlowRoutine(float scale, float duration) {
            SetTimeScale(scale);

            var t = 0f;
            while (UnityEngine.Time.timeScale < normalTimeScale) {
                if (GameRunning) {
                    t += UnityEngine.Time.unscaledDeltaTime;
                    var t01 = Mathf.Clamp01(t / duration);
                    var timeScale = scale + speedUpCurve.Evaluate(t01);
                    SetTimeScale(timeScale);
                }

                yield return null;
            }
        }

        private void SetTimeScale(float scale) {
            UnityEngine.Time.timeScale = Mathf.Clamp01(scale);
            UnityEngine.Time.fixedDeltaTime = normalFixedDeltaTime * scale;
        }

        private void ResetTimeScale() => (UnityEngine.Time.timeScale, UnityEngine.Time.fixedDeltaTime) =
            (normalTimeScale, normalFixedDeltaTime);
    }
}