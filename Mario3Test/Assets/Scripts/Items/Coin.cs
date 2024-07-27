using UnityEngine;

public class Coin : ItemInBox {
    public override void AppearFromBox() {
        CollectCoin();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Mario mario = collision.GetComponent<Mario>();

        if (mario) {
            CollectCoin();
        }
    }

    private void CollectCoin() {
        CoinManager.Instance.IncrementCoinsCounter();
        gameObject.SetActive(false);
    }
}
