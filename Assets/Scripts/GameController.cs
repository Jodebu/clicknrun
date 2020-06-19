using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Status GameStatus { get; set; } = Status.PRE_START;
    public float speed { get; set; } = 5f;
    public float scrollSpeed { get; set; } = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    public enum Status
    {
        PRE_START,
        STARTED,
        FINISH
    }
}
