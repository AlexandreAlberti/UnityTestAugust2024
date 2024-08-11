using Mario;
using UnityEngine;

namespace Environment {
    public class LevelEnd : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collision) {
            Mario.Mario mario = collision.GetComponent<Mario.Mario>();

            if (mario) {
                MarioManager.Instance.RestartLevel();
            }
        }
    }
}