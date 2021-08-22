using UBHG.Enemies;
using UBHG.Extensions;
using UnityEngine;

namespace UBHG.Common {
    public sealed class CollisionDamageDealer : MonoBehaviour {
        [Header("Parameters")]
        [Min(1)]
        [SerializeField] private int damage = 1;

        private bool isEnemy;

        private void Awake() => isEnemy = this.HasComponent<Enemy>();

        private void OnCollisionEnter2D(Collision2D collision) {
            var other = collision.gameObject;

            if (false == other.TryGetComponent<Health>(out var health)) {
                return;
            }

            if (isEnemy && health is Player.Health || !isEnemy) {
                health.TakeDamage(damage);
            }
        }
    }
}