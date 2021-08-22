using System;
using UnityEngine;
using UnityEngine.Events;

namespace UBHG.Common {
    public class Health : MonoBehaviour {
        [Header("Parameters")]
        [Min(1)]
        [SerializeField] private int maxHealth;

        [SerializeField] private DeathAction whenNoHealth = DeathAction.DoNothing;

        [Header("Events")]
        [Tooltip("Invoked when health is decreased but is still greater than 0")]
        [SerializeField] private UnityEvent damaged;

        [Tooltip("Invoked when health reaches 0")]
        [SerializeField] private UnityEvent killed;

        internal int Current { get; private set; }

        protected void Awake() => Current = maxHealth;

        public event UnityAction Damaged {
            add => damaged.AddListener(value);
            remove => damaged.RemoveListener(value);
        }

        public event UnityAction Killed {
            add => killed.AddListener(value);
            remove => killed.RemoveListener(value);
        }

        internal void TakeDamage(int damage) {
            if (false == enabled || damage <= 0) {
                return;
            }

            Current = Mathf.Clamp(Current - damage, 0, maxHealth);

            if (Current > 0) {
                damaged.Invoke();
                return;
            }

            killed.Invoke();

            ExecuteDeathAction();
        }

        public void Die() {
            Current = 0;
            killed.Invoke();
            ExecuteDeathAction();
        }
        
        private void ExecuteDeathAction() {
            switch (whenNoHealth) {
                case DeathAction.Destroy:
                    Destroy(gameObject);
                    break;
                case DeathAction.Deactivate:
                    gameObject.SetActive(false);
                    break;
                case DeathAction.DoNothing:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(whenNoHealth));
            }
        }

        private enum DeathAction {
            DoNothing,
            Destroy,
            Deactivate
        }
    }
}