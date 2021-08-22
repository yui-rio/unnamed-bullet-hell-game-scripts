using UBHG.Utility.Levels;
using UnityEngine;

namespace UBHG.Utility {
    public sealed class ResetProgress : MonoBehaviour {
        public void ResetGameProgress() => PlayerProgress.Reset();
    }
}