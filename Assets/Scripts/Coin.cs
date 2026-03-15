using UnityEngine;

public class Coin : MonoBehaviour
{
    public float CoinScore { get; } = 20.0f;

    [SerializeField] private AudioClip sonidoMoneda;
    [SerializeField]private AudioSource sfxSource;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float ConsumeCoin()
    {
        sfxSource.PlayOneShot(sonidoMoneda);
        Destroy(this.gameObject);
        return CoinScore;
    }
}
