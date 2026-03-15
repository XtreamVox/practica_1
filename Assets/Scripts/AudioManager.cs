using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Componentes de Audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Librería de Efectos")]
    [SerializeField] private List<SoundEffect> sfxLibrary;

    [System.Serializable]
    public struct SoundEffect
    {
        public string name;
        public AudioClip clip;
    }

    private void Awake()
    {
        // Patrón Singleton Persistente
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

    public void PlayMusic(AudioClip newClip)
    {
        if (musicSource.clip == newClip) return; // Ya está sonando esta pista

        musicSource.clip = newClip;
        musicSource.loop = true; // Asegura que la música sea un bucle
        musicSource.Play();
    }

    public void PlaySfx(string name)
    {
        SoundEffect effect = sfxLibrary.Find(x => x.name == name);
        
        if (effect.clip != null)
        {
            // PlayOneShot permite que varios sonidos suenen a la vez
            sfxSource.PlayOneShot(effect.clip);
        }
        else
        {
            Debug.LogWarning($"El efecto '{name}' no existe en la librería.");
        }
    }
}