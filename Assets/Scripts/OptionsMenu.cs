using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Toggle invertScrollToggle = null;
    private RectTransform menuPanel;

    private void Awake()
    {
        menuPanel = GetComponent<RectTransform>();
        menuPanel.anchoredPosition = new Vector2(menuPanel.anchoredPosition.x, Screen.height * 3);
        invertScrollToggle.isOn = PlayerPrefs.GetInt("InvertScroll", 1) < 0; ;
    }
}
