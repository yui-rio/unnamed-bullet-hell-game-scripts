using System.Collections;
using UnityEngine;

namespace UBHG.Enemies.Attacks {
    public sealed class MissileShooter : Attack {
        [Header("Parameters")]
        [SerializeField] private float rateOfFire;

        [Min(1)]
        [SerializeField] private int missilesCount;

        [SerializeField] private bool randomAngle;

        [Header("Prefabs")]
        [SerializeField] private Missile missilePrefab;

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

        private void Shoot() {
            var tf = transform;
            var step = 2f * Mathf.PI / missilesCount * Mathf.Rad2Deg;

            var startingAngle =
                randomAngle
                    ? Random.Range(0f, 2f * Mathf.PI * Mathf.Rad2Deg)
                    : tf.rotation.eulerAngles.z;

            for (var i = 0; i < missilesCount; i += 1) {
                Instantiate(
                    missilePrefab,
                    tf.position,
                    Quaternion.Euler(0f, 0f, startingAngle + i * step));
            }
        }
    }
}