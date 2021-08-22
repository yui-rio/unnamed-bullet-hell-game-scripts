using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UBHG.LevelProgress {
    public sealed class ExecuteActions : LevelActivity {
        [SerializeField] private UnityEvent actions;

        protected override IEnumerator ActivityAction() {
            actions.Invoke();
            yield return null;
        }
    }
}