using UBHG.Extensions;
using UnityEngine;
using static UBHG.Extensions.MathExtensions;

namespace UBHG.Common {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour {
        [Header("Parameters")]
        [SerializeField] private float speed;

        [Header("Prefabs")]
        [SerializeField] protected ParticleSystem particles;

        private new Rigidbody2D rigidbody2D;

        private void Awake() => rigidbody2D = GetComponent<Rigidbody2D>();

        private void Start() =>
            rigidbody2D.velocity = FromPolarDeg(transform.rotation.eulerAngles.z, speed);

        private void OnCollisionEnter2D(Collision2D collision) {
            Instantiate(particles, transform.position, collision.NormalQuaternion());
            Destroy(gameObject);
        }
    }
}