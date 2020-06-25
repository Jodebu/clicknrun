using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    private AudioSource _music;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
            _music = GetComponent<AudioSource>();
            PlayMusic(Convert.ToBoolean(PlayerPrefs.GetInt("MusicOn", 1)));
        }
        else Destroy(gameObject);
    }

    public void StartMusic() => _music.Play();

    public void PlayMusic(bool enable)
    {
        PlayerPrefs.SetInt("MusicOn", Convert.ToInt32(enable));
        if (enable) _music.Play();
        else _music.Stop();
    }
}
