using System;
using UnityEngine;

public class Mushroom : ItemInBox {
    public event EventHandler<float> OnMarioFound;

    public override void AppearFromBox() {
        StartMoving();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Mario mario = collision.GetComponent<Mario>();

        if (mario) {
            MushroomEffect(mario);
        }
    }

    private void StartMoving() {
        foreach (Mario mario in FindObjectsOfType<Mario>()) {
            if (mario.gameObject.activeSelf) {
                OnMarioFound?.Invoke(this, mario.transform.position.x);
                break;
            }
        }
    }

    private void MushroomEffect(Mario mario) {
        if (!MarioManager.Instance.CanBreakRegularBlocks()) {
            MarioManager.Instance.MushroomPowerUp();
        }

        gameObject.SetActive(false); // Deactivate mushroom
    }
}
