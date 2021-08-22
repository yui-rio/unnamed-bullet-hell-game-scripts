using System;
using JetBrains.Annotations;
using UBHG.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace UBHG.Player {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CharacterControls))]
    [RequireComponent(typeof(BulletShooter))]
    public sealed class PlayerCharacter : MonoBehaviour {
        private BulletShooter bulletShooter;
        private CharacterControls characterControls;
        private Health health;
        private new Rigidbody2D rigidbody2D;

        [CanBeNull] internal static PlayerCharacter Instance { get; private set; }

        private void Awake() {
            Instance = this;
            (rigidbody2D, health, characterControls, bulletShooter) =
                this.GetComponents<Rigidbody2D, Health, CharacterControls, BulletShooter>();
        }

        private void OnDestroy() => Instance = null;

        private void OnDisable() => Instance = null;

        public event UnityAction Killed {
            add => health.Killed += value;
            remove => health.Killed -= value;
        }

        public void Stop() {
            Instance = null;

            rigidbody2D.simulated = false;
            health.enabled = false;
            characterControls.enabled = false;
            bulletShooter.enabled = false;
        }
    }
}