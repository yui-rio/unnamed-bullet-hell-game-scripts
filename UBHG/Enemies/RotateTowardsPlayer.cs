using UBHG.Extensions;
using UBHG.Player;
using UnityEngine;

namespace UBHG.Enemies {
    public sealed class RotateTowardsPlayer : MonoBehaviour {
        [Header("Parameters")]
        [SerializeField] private float rotateSpeed;

        [SerializeField] private bool startFacingPlayer = true;

        private Transform player;

        private void Start() {
            var playerCharacter = PlayerCharacter.Instance;
            if (null == playerCharacter) {
                enabled = false;
                return;
            }

            playerCharacter.Killed += () => enabled = false;
            player = playerCharacter.transform;

            if (startFacingPlayer) {
                transform.LookAt2D(player.position);
            }
        }

        private void Update() => transform.RotateTowards2D(player.position, rotateSpeed);
    }
}