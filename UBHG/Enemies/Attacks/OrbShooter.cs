using System.Collections;
using UBHG.Common;
using UnityEngine;

namespace UBHG.Enemies.Attacks {
    public abstract class OrbShooter : Attack {
        [Header("Parameters")]
        [SerializeField] private float rateOfFire;

        [SerializeField] protected float invulnerableProbability = 0.3f;

        [Header("Prefabs")]
        [SerializeField] protected Bullet orbPrefab;

        [SerializeField] protected Bullet invulnerableOrbPrefab;

        private Coroutine shootCoroutine;

        private void Start() {
            var attackCooldown = 1f / rateOfFire;

            IEnumerator ShootRoutine() {
                while (enabled) {
                    yield return new WaitForSeconds(attackCooldown);
                    Shoot();
                }
            }

            shootCoroutine = StartCoroutine(ShootRoutine());
        }

        private void OnDisable() => StopCoroutine(shootCoroutine);

        protected abstract void Shoot();

        protected void Shoot(Bullet prefab, float angle) =>
            Instantiate(prefab, transform.position, Quaternion.Euler(0f, 0f, angle));
    }
}