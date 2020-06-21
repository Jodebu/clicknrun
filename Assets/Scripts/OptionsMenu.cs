using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private RectTransform menuPanel;

    private void Awake()
    {
        menuPanel = GetComponent<RectTransform>();
        menuPanel.anchoredPosition = new Vector2(menuPanel.anchoredPosition.x, Screen.height * 3);
    }
}
