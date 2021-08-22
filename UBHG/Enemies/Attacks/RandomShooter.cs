using UnityEngine;

namespace UBHG.Enemies.Attacks {
    public sealed class RandomShooter : OrbShooter {
        private const float FULL_ARC = 360f;
        [Range(1, FULL_ARC)]
        [SerializeField] private float arc = FULL_ARC;
        
        protected override void Shoot() {
            var angle = Random.value * arc;
            var prefab = Random.value < invulnerableProbability ? invulnerableOrbPrefab : orbPrefab;
            Shoot(prefab, transform.rotation.eulerAngles.z + angle - arc / 2f);
        }
    }
}