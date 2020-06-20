using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    private AudioSource music;

    private void Awake()
    {
        Instance = this;
        music = GetComponent<AudioSource>();
        StartMusic();
    }

    public void StartMusic() => music.Play();
}
