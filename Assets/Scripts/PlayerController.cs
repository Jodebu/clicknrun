using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool started = false;
    private bool grounded = true;

    private void Update()
    {
        if (!started && Input.GetMouseButtonDown(0))
            started = true;

        if (started)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(4, GetComponent<Rigidbody2D>().velocity.y);

            if (Input.GetMouseButtonDown(1) && grounded)
            {
                grounded = false;
                GetComponent<Rigidbody2D>().AddForce(transform.up * 800);
            }

            if (!grounded)
            {
                Vector3 vel = GetComponent<Rigidbody2D>().velocity;
                vel.y -= 50 * Time.deltaTime;
                GetComponent<Rigidbody2D>().velocity = vel;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }
}
