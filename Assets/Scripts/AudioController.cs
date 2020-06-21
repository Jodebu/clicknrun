using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    private AudioSource music;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
            music = GetComponent<AudioSource>();
            StartMusic();
        }
        else Destroy(gameObject);
    }

    public void StartMusic() => music.Play();
}
