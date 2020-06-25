using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private ParticleSystem particles = null;
    [SerializeField] private LayerMask groundLayerMask = 0;
    [SerializeField] private LayerMask scrollLayerMask = 0;

    private CapsuleCollider2D _capCollider;
    private bool _offGround = false;
    private static readonly int GameStarted = Animator.StringToHash("gameStarted");

    private void Awake()
    {
        _capCollider = GetComponent<CapsuleCollider2D>();
        Vector2 initialPos = mainCamera.ScreenToWorldPoint(Vector3.zero);
        transform.position = new Vector3(initialPos.x + 2, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (GameController.Instance.GameStatus == GameController.Status.PreStart && Input.GetMouseButtonDown(0))
        {
            GameController.Instance.GameStatus = GameController.Status.Started;
            PlayAnimations(true);
            PlayParticles(true);
        }

        if (GameController.Instance.GameStatus == GameController.Status.Started)
        {
            transform.Translate(GameController.Instance.Speed * Time.deltaTime, 0, 0);

            if (Input.GetMouseButtonDown(1) && IsGrounded())
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * 800);
                PlayParticles(false);
            }

            if (!IsGrounded())
            {
                Vector3 vel = GetComponent<Rigidbody2D>().velocity;
                vel.y -= 50 * Time.deltaTime;
                GetComponent<Rigidbody2D>().velocity = vel;
                _offGround = true;
            }
            else
            {
                if (_offGround)
                {
                    _offGround = false;
                    PlayParticles(true);
                }
            }

            if (IsOnScrollable())
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Input.mouseScrollDelta.y * GameController.Instance.ScrollSpeed, transform.position.z);
            }
        }

        if (GameController.Instance.GameStatus == GameController.Status.Finish)
        {
            PlayAnimations(false);
            PlayParticles(false);
        }
    }

    private void PlayAnimations(bool enabled)
    {
        animator.SetBool(GameStarted, enabled);
    }

    private void PlayParticles(bool enable)
    {
        if (enable && !particles.isPlaying) particles.Play();
        else if (particles.isPlaying) particles.Stop();
    }

    private bool IsGrounded()
    {
        var bounds = _capCollider.bounds;
        RaycastHit2D hit = Physics2D.CapsuleCast(bounds.center, bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, bounds.extents.y + .01f, groundLayerMask);
        return hit.collider != null || IsOnScrollable();
    }

    private bool IsOnScrollable()
    {
        var bounds = _capCollider.bounds;
        RaycastHit2D hit = Physics2D.CapsuleCast(bounds.center, bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, bounds.extents.y + .01f, scrollLayerMask);
        return hit.collider != null;
    }
}
