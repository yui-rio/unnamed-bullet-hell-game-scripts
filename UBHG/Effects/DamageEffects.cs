using UBHG.Common;
using UBHG.Extensions;
using UnityEngine;

namespace UBHG.Effects {
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(AudioSource))]
    public sealed class DamageEffects : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private ParticleSystem hitParticles;

        [Header("Assets")]
        [SerializeField] private AudioClip hitSound;

        private AudioSource audioSource;

        private void Awake() => audioSource = GetComponent<AudioSource>();

        private void Start() {
            var health = GetComponent<Health>();
            health.Damaged += CreateDamagedEffects;
        }

        private void CreateDamagedEffects() {
            audioSource.PlayWithRandomPitch(hitSound);
            hitParticles.Play();
        }
    }
}