using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Position position = Position.Left;
   
    private enum Position { Left, Bottom, Right }
    private BoxCollider2D boxCollider;
    private Camera mainCamera = null;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        mainCamera = GetComponentInParent<Camera>();

        switch (position) {
            case Position.Left:
                boxCollider.offset = new Vector2(mainCamera.ScreenToWorldPoint(Vector3.zero).x - 0.5f, boxCollider.offset.y);
                boxCollider.size = new Vector2(boxCollider.size.x, 10);
                break;
            case Position.Right:
                boxCollider.offset = new Vector2(mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 0.5f, boxCollider.offset.y);
                boxCollider.size = new Vector2(boxCollider.size.x, 10);
                break;
            case Position.Bottom:
                boxCollider.offset = new Vector2(boxCollider.offset.x, mainCamera.ScreenToWorldPoint(Vector3.zero).y - 0.5f);
                boxCollider.size = new Vector2(20, boxCollider.size.y);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameController.Instance.GameStatus = GameController.Status.FINISH;
    }
}
