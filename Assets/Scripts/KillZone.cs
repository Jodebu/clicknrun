using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Position position = Position.Left;
   
    private enum Position { Left, Bottom, Right }
    private new BoxCollider2D collider;
    private new Camera camera = null;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        camera = GetComponentInParent<Camera>();

        switch (position) {
            case Position.Left:
                collider.offset = new Vector2(camera.ScreenToWorldPoint(Vector3.zero).x - 0.5f, collider.offset.y);
                collider.size = new Vector2(collider.size.x, 10);
                break;
            case Position.Right:
                collider.offset = new Vector2(camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 0.5f, collider.offset.y);
                collider.size = new Vector2(collider.size.x, 10);
                break;
            case Position.Bottom:
                collider.offset = new Vector2(collider.offset.x, camera.ScreenToWorldPoint(Vector3.zero).y - 0.5f);
                collider.size = new Vector2(20, collider.size.y);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameController.Instance.GameStatus = GameController.Status.FINISH;
    }
}
