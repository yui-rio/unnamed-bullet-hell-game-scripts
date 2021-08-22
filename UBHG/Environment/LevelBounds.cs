using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace UBHG.Environment {
    [ExecuteAlways]
    public sealed class LevelBounds : MonoBehaviour {
        private const float BOUNDS_WIDTH = 1;
        private const int POSITION_COUNT = 5;

        [Header("Parameters")]
        [Header("Size")]
        [SerializeField] private float width;

        [SerializeField] private float height;

        [Header("Visuals")]
        [SerializeField] private Color color;

        [SerializeField] private float lineThickness;

        [Header("Components")]
        [SerializeField] private BoxCollider2D topCollider;

        [SerializeField] private BoxCollider2D bottomCollider;

        [SerializeField] private BoxCollider2D leftCollider;

        [SerializeField] private BoxCollider2D rightCollider;

        [SerializeField] private CompositeCollider2D compositeCollider2D;

        [SerializeField] private LineRenderer lineRenderer;

        private void Update() {
            if (Application.IsPlaying(gameObject)) {
                enabled = false;
                return;
            }

            DrawBounds();
            CreateBoundingBox();
        }

        private void DrawBounds() {
            var offsetX = width / 2;
            var offsetY = height / 2;

            lineRenderer.positionCount = POSITION_COUNT;

            var positions = new List<Vector3> {
                new Vector2(-offsetX, offsetY),
                new Vector2(offsetX, offsetY),
                new Vector2(offsetX, -offsetY),
                new Vector2(-offsetX, -offsetY),
                new Vector2(-offsetX, offsetY)
            };

            Assert.AreEqual(positions.Count, POSITION_COUNT);

            for (var i = 0; i < POSITION_COUNT; i += 1) {
                lineRenderer.SetPosition(i, positions[i] + transform.position);
            }

            lineRenderer.startWidth = lineThickness;
            lineRenderer.endWidth = lineThickness;

            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }

        private void CreateBoundingBox() {
            var offsetX = width / 2 + BOUNDS_WIDTH / 2f;
            var offsetY = height / 2 + BOUNDS_WIDTH / 2f;

            topCollider.offset = Vector2.up * offsetY;
            bottomCollider.offset = Vector2.down * offsetY;
            leftCollider.offset = Vector2.left * offsetX;
            rightCollider.offset = Vector2.right * offsetX;

            var topAndBottomSize = new Vector2(width + BOUNDS_WIDTH * 2, BOUNDS_WIDTH);
            var leftAndRightSize = new Vector2(BOUNDS_WIDTH, height);

            (topCollider.size, bottomCollider.size) = (topAndBottomSize, topAndBottomSize);
            (leftCollider.size, rightCollider.size) = (leftAndRightSize, leftAndRightSize);

            topCollider.usedByComposite = true;
            bottomCollider.usedByComposite = true;
            leftCollider.usedByComposite = true;
            rightCollider.usedByComposite = true;

            compositeCollider2D.geometryType = CompositeCollider2D.GeometryType.Polygons;
        }
    }
}