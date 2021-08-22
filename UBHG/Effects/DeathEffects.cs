using UBHG.Common;
using UnityEngine;

namespace UBHG.Effects {
    [RequireComponent(typeof(Health))]
    public sealed class DeathEffects : MonoBehaviour {
        [Header("Prefabs")]
        [SerializeField] private ParticleSystem deathParticles;

        private void Start() {
            var health = GetComponent<Health>();
            health.Killed += CreateParticles;
        }

        private void CreateParticles() => Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
}