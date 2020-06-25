using UnityEngine;

public class Eraser : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) => Destroy(collision.gameObject);
}
