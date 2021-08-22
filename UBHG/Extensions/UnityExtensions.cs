using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UBHG.Extensions {
    internal static class UnityExtensions {
        internal static bool HasComponent<T>(this Component component) where T : Component =>
            component.gameObject.HasComponent<T>();


        internal static bool HasComponent<T>(this GameObject gameObject) where T : Component =>
            gameObject.TryGetComponent<T>(out _);


        internal static Vector3 MousePosition(Camera perspectiveCamera) {
            var cameraPosition = perspectiveCamera.transform.position;
            var mouseInput = new Vector3(Screen.width, Screen.height, 0f) - Input.mousePosition;

            mouseInput.z = cameraPosition.z;
            cameraPosition.z = 0;

            var position = perspectiveCamera.ScreenToWorldPoint(mouseInput);
            position.z = 0;

            return position;
        }

        /// <summary>
        ///     Note: original pitch is NOT restored
        /// </summary>
        internal static void PlayWithRandomPitch(
            this AudioSource audioSource,
            AudioClip audioClip,
            float minPitch = .75f,
            float maxPitch = 1f) {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(audioClip);
        }

        private static float NormalAngleDeg(this Collision2D collision) =>
            0 == collision.contactCount
                ? 0f
                : collision.GetContact(0).normal.ToAngleDeg();


        internal static Quaternion NormalQuaternion(this Collision2D collision) =>
            Quaternion.Euler(0f, 0f, collision.NormalAngleDeg());


        internal static Tuple<T1, T2> GetComponents<T1, T2>(this Component component)
            where T1 : Component
            where T2 : Component => new Tuple<T1, T2>(component.GetComponent<T1>(), component.GetComponent<T2>());


        internal static Tuple<T1, T2, T3, T4> GetComponents<T1, T2, T3, T4>(this Component component)
            where T1 : Component
            where T2 : Component =>
            new Tuple<T1, T2, T3, T4>(
                component.GetComponent<T1>(),
                component.GetComponent<T2>(),
                component.GetComponent<T3>(),
                component.GetComponent<T4>());

        public static void LookAt2D(this Transform transform, Vector3 target) {
            var targetDirection = target - transform.position;
            var targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
        }

        public static void RotateTowards2D(this Transform transform, Vector3 target, float rotateSpeed) {
            var targetDirection = (target - transform.position).normalized;
            var rotationToTarget = Quaternion.FromToRotation(transform.right, targetDirection);

            transform.rotation *= Quaternion.RotateTowards(
                Quaternion.identity,
                rotationToTarget,
                rotateSpeed * Time.deltaTime);
        }

        public static void RotateTowards2D(this Rigidbody2D rigidbody2D, Vector2 target, float rotateSpeed) {
            var targetDirection = (target - rigidbody2D.position).normalized;
            var rotationToTarget = Quaternion.FromToRotation(
                rigidbody2D.velocity, targetDirection);
            var angleDelta = Quaternion.RotateTowards(
                Quaternion.identity,
                rotationToTarget,
                rotateSpeed * Time.deltaTime);
            rigidbody2D.velocity = angleDelta * rigidbody2D.velocity;
            rigidbody2D.rotation += angleDelta.eulerAngles.z;
        }
    }
}