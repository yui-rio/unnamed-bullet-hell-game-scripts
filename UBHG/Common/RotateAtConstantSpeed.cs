using UnityEngine;

namespace UBHG.Common {
    public sealed class RotateAtConstantSpeed : MonoBehaviour {
        [Header("Parameters")]
        [SerializeField] private float rotateSpeed;

        private void Update() =>
            transform.rotation =
                Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + rotateSpeed * Time.deltaTime);
    }
}