using Mario;
using System;
using UnityEngine;

namespace Items {
    public class Mushroom : ItemInBox {
        public event EventHandler<float> OnMarioFound;

        public override void AppearFromBox() {
            StartMoving();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Mario.Mario mario = collision.GetComponent<Mario.Mario>();

            if (mario) {
                MushroomEffect(mario);
            }
        }

        private void StartMoving() {
            foreach (Mario.Mario mario in FindObjectsOfType<Mario.Mario>()) {
                if (mario.gameObject.activeSelf) {
                    OnMarioFound?.Invoke(this, mario.transform.position.x);
                    break;
                }
            }
        }

        private void MushroomEffect(Mario.Mario mario) {
            if (!MarioManager.Instance.CanBreakRegularBlocks()) {
                MarioManager.Instance.MushroomPowerUp();
            }

            gameObject.SetActive(false); // Deactivate mushroom
        }
    }
}