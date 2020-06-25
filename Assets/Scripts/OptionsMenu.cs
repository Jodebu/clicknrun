using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Toggle invertScrollToggle = null;
    [SerializeField] private Toggle playMusicToggle = null;
    private RectTransform _menuPanel;

    private void Awake()
    {
        _menuPanel = GetComponent<RectTransform>();
        _menuPanel.anchoredPosition = new Vector2(_menuPanel.anchoredPosition.x, Screen.height * 3);
        invertScrollToggle.isOn = PlayerPrefs.GetInt("InvertScroll", 1) < 0; ;
        playMusicToggle.isOn = PlayerPrefs.GetInt("MusicOn", 1) > 0; ;
    }
}
