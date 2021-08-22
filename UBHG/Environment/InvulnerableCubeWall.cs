using UnityEngine;

namespace UBHG.Environment {
    public sealed class InvulnerableCubeWall : MonoBehaviour {
        private InvulnerableCube[] children;

        private bool isBeingDestroyed;

        private void Start() => children = GetComponentsInChildren<InvulnerableCube>();

        public void Destroy() {
            if (isBeingDestroyed) {
                return;
            }

            isBeingDestroyed = true;

            foreach (var child in children) {
                child.Destroy();
            }

            transform.DetachChildren();
            Destroy(gameObject);
        }
    }
}