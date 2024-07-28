using System;
using UnityEngine;

public class Mushroom : ItemInBox {

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private CircleCollider2D _circleCollider2D;
    [SerializeField] private BoxCollider2D _boxCollider2D;

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
        if (!mario.CanBreakRegularBlocks()) {
            // TODO - Trigger Mario Grow Animation
        }

        gameObject.SetActive(false); // Deactivate mushroom
    }
}
