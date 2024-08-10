using UnityEngine;

namespace Environment {
    public class TraspassableFloor : MonoBehaviour {
        [SerializeField] private BoxCollider2D _floorCollider;

        private void OnTriggerEnter2D(Collider2D collision) {
            Mario.Mario mario = collision.transform.GetComponent<Mario.Mario>();

            if (mario) {
                _floorCollider.enabled = mario.GetComponent<Rigidbody2D>().velocity.y < 0.0f;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            Mario.Mario mario = collision.transform.GetComponent<Mario.Mario>();

            if (mario) {
                _floorCollider.enabled = true;
            }
        }
    }
}