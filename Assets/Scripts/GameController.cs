using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Status GameStatus { get; set; } = Status.PreStart;
    public float Speed { get; private set; } = 5f;
    public float ScrollSpeed { get; set; } = 0.5f;

    [SerializeField] private RectTransform optionsPanel = null;
    private bool _isOptionsOpen = false;
    private Status _previousStatus = Status.PreStart;

    public enum Status
    {
        PreStart,
        Started,
        Paused,
        Finish
    }

    private void Awake()
    {
        Instance = this;
        ScrollSpeed = PlayerPrefs.GetInt("InvertScroll", 1) * Math.Abs(ScrollSpeed);
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

        if (_isOptionsOpen)
        {
            newStatus = _previousStatus;
            targetHeight = optionsPanel.position.y + Screen.height * 3;
        }
        else
        {
            _previousStatus = GameStatus;
            newStatus = Status.Paused;
            targetHeight = 0f;
        }

        _isOptionsOpen = !_isOptionsOpen;
        optionsPanel.LeanMoveY(targetHeight, 0.3f).setEaseInOutExpo();

        return newStatus;
    }

    public void OnInvertScrollToggle(bool invert)
    {
        int sign = invert ? -1 : 1;
        PlayerPrefs.SetInt("InvertScroll", sign);
        ScrollSpeed = sign * Math.Abs(ScrollSpeed);
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
        Speed *= 1.2f;
    }
}
