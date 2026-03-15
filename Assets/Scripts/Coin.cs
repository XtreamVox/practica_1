using UnityEngine;

public class Coin : MonoBehaviour
{
    public int CoinScore { get; } = 20;
    private string coinID;

    void Awake()
    {
        // Generamos un ID único basado en la posición de la moneda en el mundo
        coinID = transform.position.ToString();
    }
    
    void Start()
    {
        // Si esta moneda ya fue recogida en una sesión anterior, la destruimos inmediatamente
        if (GameData.Instance != null && GameData.Instance.IsCoinCollected(coinID))
        {
            Destroy(this.gameObject);
        }
    }

    public int ConsumeCoin()
    {
        // Marcamos la moneda como recogida antes de destruirla
        if (GameData.Instance != null)
        {
            GameData.Instance.MarkCoinAsCollected(coinID);
        }

        AudioManager.Instance.PlaySfx("CoinSfx");
        Destroy(this.gameObject);
        return CoinScore;
    }
}