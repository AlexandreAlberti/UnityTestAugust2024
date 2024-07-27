using UnityEngine;

public class Coin : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        Mario mario = collision.GetComponent<Mario>();

        if (mario) {
            CoinManager.Instance.IncrementCoinsCounter();
            Destroy(gameObject);
        }
    }
}
