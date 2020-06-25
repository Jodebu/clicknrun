using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D cursor = null;

    private void Awake()
    {
        Cursor.SetCursor(cursor, new Vector2(0.2f, 0.3f), CursorMode.Auto);
    }
}
