using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UBHG.UI.Buttons {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TextHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [Header("Parameters")]
        [SerializeField] private float sizeMultiplier;

        [Header("Events")]
        [SerializeField] private UnityEvent hoverEnter;

        private float hoverFontSize;

        private float normalFontSize;

        private TextMeshProUGUI text;

        private void Awake() => text = GetComponent<TextMeshProUGUI>();

        private void Start() {
            normalFontSize = text.fontSize;
            hoverFontSize = normalFontSize * sizeMultiplier;
        }

        public void OnPointerEnter(PointerEventData _) {
            hoverEnter.Invoke();
            text.fontSize = hoverFontSize;
        }

        public void OnPointerExit(PointerEventData _) => text.fontSize = normalFontSize;
    }
}