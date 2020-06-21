using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private ParticleSystem particles = null;
    [SerializeField] private LayerMask groundLayerMask = 0;
    [SerializeField] private LayerMask scrollLayerMask = 0;

    private CapsuleCollider2D capCollider;

    private void Awake()
    {
        capCollider = GetComponent<CapsuleCollider2D>();
        Vector2 initialPos = mainCamera.ScreenToWorldPoint(Vector3.zero);
        transform.position = new Vector3(initialPos.x + 2, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (GameController.Instance.GameStatus == GameController.Status.PRE_START && Input.GetMouseButtonDown(0))
        {
            GameController.Instance.GameStatus = GameController.Status.STARTED;
            PlayAnimations(true);
            PlayParticles(true);
        }

        if (GameController.Instance.GameStatus == GameController.Status.STARTED)
        {
            transform.Translate(GameController.Instance.speed * Time.deltaTime, 0, 0);

            if (IsGrounded())
                if (!particles.isEmitting) PlayParticles(true);

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
            }

            if (IsOnScrollable())
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Input.mouseScrollDelta.y * GameController.Instance.scrollSpeed, transform.position.z);
            }
        }

        if (GameController.Instance.GameStatus == GameController.Status.FINISH)
        {
            PlayAnimations(false);
            PlayParticles(false);
        }

        if (Input.GetMouseButtonDown(2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void PlayAnimations(bool enabled)
    {
        animator.SetBool("gameStarted", enabled);
    }

    private void PlayParticles(bool enable)
    {
        if (enable) particles.Play();
        else particles.Stop();
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.CapsuleCast(capCollider.bounds.center, capCollider.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, capCollider.bounds.extents.y + .01f, groundLayerMask);
        return hit.collider != null || IsOnScrollable();
    }

    private bool IsOnScrollable()
    {
        RaycastHit2D hit = Physics2D.CapsuleCast(capCollider.bounds.center, capCollider.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, capCollider.bounds.extents.y + .01f, scrollLayerMask);
        return hit.collider != null;
    }
}
