using UnityEngine;

namespace UBHG.Utility {
    public sealed class QuitApplication : MonoBehaviour {
        public void Quit() {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}