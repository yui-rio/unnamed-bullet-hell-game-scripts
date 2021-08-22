using UBHG.Common;
using UBHG.Extensions;
using UnityEngine;

namespace UBHG.Enemies {
    public sealed class InvulnerableOrb : Bullet {
        private void OnCollisionEnter2D(Collision2D collision) {
            var other = collision.gameObject;

            if (other.HasComponent<Player.Bullet>()) {
                return;
            }

            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}