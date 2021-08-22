using UnityEngine;

namespace UBHG.Enemies.Attacks {
    public sealed class SpiralShooter : OrbShooter {
        [Header("Spiral parameters")]
        [Min(1)]
        [SerializeField] private int leavesCount;

        protected override void Shoot() {
            var step = 2f * Mathf.PI / leavesCount * Mathf.Rad2Deg;
            var prefab = Random.value < invulnerableProbability ? invulnerableOrbPrefab : orbPrefab;

            for (var i = 0; i < leavesCount; i += 1) {
                var angle = i * step;
                Shoot(prefab, transform.rotation.eulerAngles.z + angle);
            }
        }
    }
}