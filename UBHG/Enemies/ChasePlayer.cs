using System;
using UBHG.Player;
using UnityEngine;

namespace UBHG.Enemies {
    public sealed class ChasePlayer : MonoBehaviour {
        [Header("Parameters")]
        [SerializeField] private float chaseSpeed;

        private Action move;

        private Transform player;

        private new Transform transform;

        private void Awake() => transform = GetComponent<Transform>();

        private void Start() {
            var playerCharacter = PlayerCharacter.Instance;

            if (null == playerCharacter) {
                move = DoNotMove;
                enabled = false;
                return;
            }

            player = playerCharacter.transform;

            playerCharacter.Killed += () => enabled = false;

            move = TryGetComponent<RotateTowardsPlayer>(out var rotateTowardsPlayer) && rotateTowardsPlayer.enabled
                ? (Action) MoveForward
                : MoveToPlayer;
        }

        private void Update() => move();

        private static void DoNotMove() { }

        private void MoveForward() =>
            transform.Translate(transform.right * (chaseSpeed * Time.deltaTime), Space.World);

        private void MoveToPlayer() {
            var position = transform.position;
            var direction = (player.position - position).normalized;

            transform.Translate(direction * (chaseSpeed * Time.deltaTime), Space.World);
        }
    }
}