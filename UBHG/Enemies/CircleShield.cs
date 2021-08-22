using UnityEngine;

namespace UBHG.Enemies {
    public sealed class CircleShield : MonoBehaviour {
        [Header("Prefabs")]
        [SerializeField] private ParticleSystem deathParticles;

        public void Destroy() {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}