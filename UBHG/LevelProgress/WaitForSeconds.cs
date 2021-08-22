using System.Collections;
using UnityEngine;

namespace UBHG.LevelProgress {
    public sealed class WaitForSeconds  : LevelActivity {
        [Min(0f)]
        [SerializeField] private float duration = 1f;
        
        protected override IEnumerator ActivityAction() {
            yield return new UnityEngine.WaitForSeconds(duration);
        }
    }
}