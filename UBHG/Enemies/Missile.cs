using System;
using UBHG.Common;
using UBHG.Extensions;
using UBHG.Player;
using UnityEngine;
using static UBHG.Extensions.MathExtensions;
using Health = UBHG.Common.Health;

namespace UBHG.Enemies {
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Missile : MonoBehaviour {
        [Header("Parameters")]
        [SerializeField] private float speed = 2f;

        [SerializeField] private float rotateSpeed = 120f;

        private Health health;

        private new Rigidbody2D rigidbody2D;

        private Transform player;

        private bool playerAlive = true;

        private void Awake() => (health, rigidbody2D) = this.GetComponents<Health, Rigidbody2D>();

        private void Start() {
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            rigidbody2D.useFullKinematicContacts = true;

            rigidbody2D.velocity = FromPolarDeg(transform.rotation.eulerAngles.z, speed);

            var playerCharacter = PlayerCharacter.Instance;
            if (null == playerCharacter) {
                playerAlive = false;
                return;
            }

            playerCharacter.Killed += () => playerAlive = false;
            player = playerCharacter.transform;
        }

        private void FixedUpdate() {
            if (playerAlive) {
                rigidbody2D.RotateTowards2D(player.position, rotateSpeed);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            var other = collision.gameObject;
            if (other.HasComponent<CollisionDamageDealer>()) {
                return;
            }

            health.Die();
        }
    }
}