using Mario;
using System;
using UnityEngine;

namespace Items {
    public class Leaf : ItemInBox {
        public event EventHandler OnStartMoving;

        public override void AppearFromBox() {
            OnStartMoving?.Invoke(this, EventArgs.Empty);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Mario.Mario mario = collision.GetComponent<Mario.Mario>();

            if (mario) {
                LeafEffect(mario);
            }
        }

        private void LeafEffect(Mario.Mario mario) {
            if (MarioManager.Instance.CanTransformToTanooki()) {
                MarioManager.Instance.LeafPowerUp();
            }

            gameObject.SetActive(false); // Deactivate Leaf
        }
    }
}