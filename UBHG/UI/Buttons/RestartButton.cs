using UBHG.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace UBHG.UI.Buttons {
    [RequireComponent(typeof(Button))]
    public sealed class RestartButton : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] private SceneSwitcher sceneSwitcher;

        private void Start() => GetComponent<Button>().onClick.AddListener(sceneSwitcher.RestartScene);
    }
}