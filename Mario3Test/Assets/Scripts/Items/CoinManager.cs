using UnityEngine;

public class CoinManager : MonoBehaviour {
    public static CoinManager Instance { get; private set; }

    private const string COIN_KEY = "Coins";

    private int coinCounter;

    private void Awake() {
        Instance = this;
        coinCounter = PlayerPrefs.GetInt(COIN_KEY, 0);
    }

    public void IncrementCoinsCounter() {
        coinCounter++;
        // TODO: Manage live aggregation by coins here
        PlayerPrefs.SetInt(COIN_KEY, coinCounter);
    }

}
