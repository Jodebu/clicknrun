using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Status GameStatus { get; set; } = Status.PRE_START;
    public float speed { get; private set; } = 5f;
    public float scrollSpeed { get; set; } = 0.5f;

    public enum Status
    {
        PRE_START,
        STARTED,
        FINISH
    }

    private void Awake()
    {
        Instance = this;
    }

    internal void SpeedUp()
    {
        speed *= 1.2f;
    }
}
