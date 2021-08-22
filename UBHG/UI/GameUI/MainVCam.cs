using Cinemachine;
using UBHG.Player;
using UnityEngine;

namespace UBHG.UI.GameUI {
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public sealed class MainVCam : MonoBehaviour {
        private void Start() {
            var player = PlayerCharacter.Instance;
            if (null == player) {
                return;
            }

            var cam = GetComponent<CinemachineVirtualCamera>();
            cam.Follow = player.transform;
        }
    }
}