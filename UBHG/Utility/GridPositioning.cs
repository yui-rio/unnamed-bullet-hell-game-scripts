using UnityEngine;

namespace UBHG.Utility {
    [ExecuteAlways]
    public sealed class GridPositioning : MonoBehaviour {
        [Header("Parameters")]
        [SerializeField] private float cellSizeX = 0.68f;

        [SerializeField] private float cellSizeY = 0.68f;

        private void Start() => FixPosition();

        private void Update() {
            if (Application.IsPlaying(gameObject)) {
                enabled = false;
                return;
            }

            FixPosition();
        }

        private void FixPosition() {
            var position = transform.position;
            transform.position = new Vector2 {
                x = FixPosition(position.x, cellSizeX),
                y = FixPosition(position.y, cellSizeY)
            };
        }

        private static float FixPosition(float position, float cellSize) =>
            Mathf.Sign(position) * cellSize * Mathf.RoundToInt(Mathf.Abs(position) / cellSize);
    }
}