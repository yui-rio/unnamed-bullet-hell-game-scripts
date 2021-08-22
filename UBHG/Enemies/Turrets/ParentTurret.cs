using System.Collections;
using System.Linq;
using UBHG.Common;
using UnityEngine;
using UnityEngine.Events;

namespace UBHG.Enemies.Turrets {
    public sealed class ParentTurret : MonoBehaviour {
        private const float DELAY = 1f;

        private int childrenCount;

        [SerializeField] private UnityEvent childrenKilled;

        private void Awake() {
            var children =
                GetComponentsInChildren<Health>()
                    .Where(child => false == ReferenceEquals(gameObject, child.gameObject));

            foreach (var child in children) {
                childrenCount += 1;
                child.Killed += ChildKilled;
            }
        }

        private void AllChildrenKilled() {
            IEnumerator FireEventLater() {
                yield return new WaitForSeconds(DELAY);
                childrenKilled.Invoke();
            }

            StartCoroutine(FireEventLater());
        }

        private void ChildKilled() {
            childrenCount -= 1;

            if (0 == childrenCount) {
                AllChildrenKilled();
            }
        }
    }
}