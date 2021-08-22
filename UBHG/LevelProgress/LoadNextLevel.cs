using System.Collections;
using UBHG.Extensions;
using UBHG.Utility;
using UnityEngine;

namespace UBHG.LevelProgress {
    [RequireComponent(typeof(AudioSource))]
    public sealed class LoadNextLevel : LevelActivity {
        [Header("Assets")]
        [SerializeField] private AudioClip winJingle;

        [Header("Dependencies")]
        [SerializeField] private SceneSwitcher sceneSwitcher;

        private AudioSource audioSource;

        private new void Awake() {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        protected override IEnumerator ActivityAction() {
            audioSource.PlayWithRandomPitch(winJingle);
            sceneSwitcher.LoadNextScene();
            yield return null;
        }
    }
}