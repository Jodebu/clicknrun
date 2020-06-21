using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Status GameStatus { get; set; } = Status.PRE_START;
    public float speed { get; private set; } = 5f;
    public float scrollSpeed { get; set; } = 0.5f;

    [SerializeField] private RectTransform optionsPanel = null;
    private bool isOptionsOpen = false;
    private Status previousStatus = Status.PRE_START;

    public enum Status
    {
        PRE_START,
        STARTED,
        PAUSED,
        FINISH
    }

    private void Awake()
    {
        Instance = this;
        scrollSpeed = PlayerPrefs.GetInt("InvertScroll", 1) * Math.Abs(scrollSpeed);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
            GameStatus = ToggleOptions();
    }

    public Status ToggleOptions()
    {
        Status newStatus;
        float targetHeight;

        if (isOptionsOpen)
        {
            newStatus = previousStatus;
            targetHeight = optionsPanel.position.y + Screen.height * 3;
        }
        else
        {
            previousStatus = GameStatus;
            newStatus = Status.PAUSED;
            targetHeight = 0f;
        }

        isOptionsOpen = !isOptionsOpen;
        optionsPanel.LeanMoveY(targetHeight, 0.3f).setEaseInOutExpo();

        return newStatus;
    }

    public void OnInvertScrollToggle(bool invert)
    {
        int sign = invert ? -1 : 1;
        PlayerPrefs.SetInt("InvertScroll", sign);
        scrollSpeed = sign * Math.Abs(scrollSpeed);
    }

    public void OnResumeClicked()
    {
        GameStatus = ToggleOptions();
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    internal void SpeedUp()
    {
        speed *= 1.2f;
    }
}
