using UBHG.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace UBHG.UI.Buttons {
    [RequireComponent(typeof(Button))]
    public sealed class SceneTransitionButton : MonoBehaviour {
        [Header("Scene transition parameters")]
        [SerializeField] private string sceneName;

        [Header("Dependencies")]
        [SerializeField] private SceneSwitcher sceneSwitcher;

        private void Start() {
            if (string.IsNullOrEmpty(sceneName)) {
                var message =
                    $"\"{nameof(sceneName)}\" is not set " +
                    $"(required by {typeof(SceneTransitionButton).FullName} in {name}";
                Debug.LogError(message);
                return;
            }

            GetComponent<Button>().onClick.AddListener(() => sceneSwitcher.SwitchScenes(sceneName));
        }
    }
}