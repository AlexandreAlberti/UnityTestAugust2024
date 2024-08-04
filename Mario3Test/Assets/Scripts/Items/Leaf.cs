using System;
using UnityEngine;

public class Leaf : ItemInBox {
    public event EventHandler OnStartMoving;

    public override void AppearFromBox() {
        OnStartMoving?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Mario mario = collision.GetComponent<Mario>();

        if (mario) {
            LeafEffect(mario);
        }
    }

    private void LeafEffect(Mario mario) {
        if (MarioManager.Instance.CanTransformToTanooki()) {
            MarioManager.Instance.LeafPowerUp();
        }

        gameObject.SetActive(false); // Deactivate Leaf
    }
}
