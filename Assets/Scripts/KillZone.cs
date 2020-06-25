using System;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Position position = Position.Left;
    [SerializeField] private ParticleSystem dieParticles = null;
   
    private enum Position { Left, Bottom, Right }
    private BoxCollider2D _boxCollider;
    private Camera _mainCamera;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _mainCamera = GetComponentInParent<Camera>();

        switch (position) {
            case Position.Left:
                _boxCollider.offset = new Vector2(_mainCamera.ScreenToWorldPoint(Vector3.zero).x - 0.5f, _boxCollider.offset.y);
                _boxCollider.size = new Vector2(_boxCollider.size.x, 10);
                break;
            case Position.Right:
                _boxCollider.offset = new Vector2(_mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 0.5f, _boxCollider.offset.y);
                _boxCollider.size = new Vector2(_boxCollider.size.x, 10);
                break;
            case Position.Bottom:
                _boxCollider.offset = new Vector2(_boxCollider.offset.x, _mainCamera.ScreenToWorldPoint(Vector3.zero).y - 0.5f);
                _boxCollider.size = new Vector2(20, _boxCollider.size.y);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Instantiate(dieParticles, collision.gameObject.transform.position, Quaternion.identity);
        Destroy(collision.gameObject);
        GameController.Instance.GameStatus = GameController.Status.Finish;
    }
}
