using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource sfxSource;
    public static AudioManager Manager { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
            Destroy(this.gameObject);
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySfx(AudioClip clipToPlay)
    {
        sfxSource.PlayOneShot(clipToPlay);
    }
}
