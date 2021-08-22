using System;
using UBHG.Utility.Time;
using UnityEngine;
using static UBHG.Extensions.UnityExtensions;

namespace UBHG.Player {
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class CharacterControls : MonoBehaviour {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        [Header("Movement")]
        [SerializeField] private float speed;

        [Header("Dependencies")]
        [SerializeField] private new Camera camera;

        [SerializeField] private TimeScaler timeScaler;

        private Vector2 moveInput;

        private new Rigidbody2D rigidbody2D;

        private float sqrSpeed;
        private new Transform transform;

        private void Awake() {
            (rigidbody2D, transform) = this.GetComponents<Rigidbody2D, Transform>();
            sqrSpeed = speed * speed;
        }

        private void Update() {
            ReadInput();

            if (timeScaler.GameRunning) {
                transform.LookAt2D(MousePosition(camera));
            }
        }

        private void FixedUpdate() {
            var velocity = moveInput * speed;
            rigidbody2D.velocity = velocity;
        }

        private void OnDisable() => moveInput = Vector2.zero;

        private void ReadInput() {
            (moveInput.x, moveInput.y) = (Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));

            if (moveInput.sqrMagnitude <= sqrSpeed) {
                return;
            }

            moveInput.Normalize();
        }
    }
}