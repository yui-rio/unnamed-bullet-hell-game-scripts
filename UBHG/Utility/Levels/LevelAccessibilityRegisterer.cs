using UnityEngine;

namespace UBHG.Utility.Levels {
    public sealed class LevelAccessibilityRegisterer : MonoBehaviour {
        private void Start() => PlayerProgress.Update();
    }
}