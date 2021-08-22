using System;
using UBHG.Extensions;
using UBHG.Utility.Time;
using UnityEngine;

namespace UBHG.Player {
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(AudioSource))]
    public sealed class BulletShooter : MonoBehaviour {
        private const float BULLET_OFFSET = 0.3f;
        private const int LEFT_MOUSE_BUTTON = 0;

        [Header("Parameters")]
        [SerializeField] private float rateOfFire;

        [Header("Prefabs")]
        [SerializeField] private Bullet bulletPrefab;

        [Header("Assets")]
        [SerializeField] private AudioClip shootSound;

        [Header("Dependencies")]
        [SerializeField] private TimeScaler timeScaler;

        private AudioSource audioSource;

        private float cooldown;
        private float lastFired = float.MinValue;

        private new Transform transform;

        private bool Ready => lastFired + cooldown <= Time.time;

        private void Awake() {
            (transform, audioSource) = this.GetComponents<Transform, AudioSource>();
            cooldown = 1f / rateOfFire;
        }

        private void Update() {
            if (false == Ready || false == Input.GetMouseButton(LEFT_MOUSE_BUTTON) || timeScaler.GamePaused) {
                return;
            }

            Shoot();
        }

        private void Shoot() {
            lastFired = Time.time;
            Instantiate(
                bulletPrefab,
                transform.position + transform.right * BULLET_OFFSET,
                transform.rotation);
            audioSource.PlayWithRandomPitch(shootSound);
        }
    }
}