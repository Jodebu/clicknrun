using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool grounded = true;
    private bool onScrollable = false;
    [SerializeField] private Camera camera = null;

    private void Awake()
    {
        Vector2 initialPos = camera.ScreenToWorldPoint(Vector3.zero);
        transform.position = new Vector3(initialPos.x + 2, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (GameController.Instance.GameStatus == GameController.Status.PRE_START && Input.GetMouseButtonDown(0))
            GameController.Instance.GameStatus = GameController.Status.STARTED;

        if (GameController.Instance.GameStatus == GameController.Status.STARTED)
        {
            transform.Translate(GameController.Instance.speed * Time.deltaTime, 0, 0);

            if (Input.GetMouseButtonDown(1) && OnTheGround())
                GetComponent<Rigidbody2D>().AddForce(transform.up * 800);

            if (!OnTheGround())
            {
                Vector3 vel = GetComponent<Rigidbody2D>().velocity;
                vel.y -= 50 * Time.deltaTime;
                GetComponent<Rigidbody2D>().velocity = vel;
            }

            if (onScrollable)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Input.mouseScrollDelta.y * GameController.Instance.scrollSpeed, transform.position.z);
            }
        }
    }

    private bool OnTheGround() => grounded || onScrollable;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;

        if (collision.gameObject.CompareTag("Scroll"))
            onScrollable = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = false;

        if (collision.gameObject.CompareTag("Scroll"))
            onScrollable = false;
    }
}
