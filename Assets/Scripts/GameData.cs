using System;
using UnityEngine;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    [SerializeField] private int vidas = 3;
    [SerializeField] private int puntuacion = 0;

    public event Action OnVidasChanged;
    public event Action OnPuntosChanged;
    public int Vidas => vidas;
    public int Puntuacion => puntuacion;

    private HashSet<string> collectedCoins = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void MarkCoinAsCollected(string id) => collectedCoins.Add(id);
    
    public bool IsCoinCollected(string id) => collectedCoins.Contains(id);
    
    public void SumarPuntos(int puntos) 
    {
        puntuacion += puntos;
        OnPuntosChanged?.Invoke();
    }

    public void RestarVida() 
    {
        vidas--;
        OnVidasChanged?.Invoke(); // Notifica a los suscriptores
    }
    
    public void ResetProgress()
    {
        collectedCoins.Clear();
        vidas = 3;
        puntuacion = 0;
        // Notificamos a quien esté escuchando que todo cambió
        OnVidasChanged?.Invoke();
        OnPuntosChanged?.Invoke();
        
        Debug.Log("Progreso reseteado correctamente.");
    }
}