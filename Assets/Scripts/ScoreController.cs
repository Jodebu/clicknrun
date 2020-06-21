using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private Animator speedUp = null;

    private TextMeshProUGUI score;
    private float offset;
    private int nextSpeedUp = 100;

    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        offset = player.position.x;
    }

    private void Update()
    {
        if (GameController.Instance.GameStatus == GameController.Status.STARTED)
        {
            int distance = GetDistanceTraveled();
            score.text = distance.ToString();
            if (distance > nextSpeedUp)
            {
                nextSpeedUp *= 2;
                GameController.Instance.SpeedUp();
                speedUp.Play("speed_up", -1, 0f);
            }
        }
    }

    private int GetDistanceTraveled() => (int)(player.position.x - offset);
}
