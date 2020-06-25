using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private Animator speedUp = null;

    private TextMeshProUGUI _score;
    private float _offset;
    private int _nextSpeedUp = 100;

    private void Start()
    {
        _score = GetComponent<TextMeshProUGUI>();
        _offset = player.position.x;
    }

    private void Update()
    {
        if (GameController.Instance.GameStatus == GameController.Status.Started)
        {
            int distance = GetDistanceTraveled();
            _score.text = distance.ToString();
            if (distance > _nextSpeedUp)
            {
                _nextSpeedUp *= 2;
                GameController.Instance.SpeedUp();
                speedUp.Play("speed_up", -1, 0f);
            }
        }
    }

    private int GetDistanceTraveled() => (int)(player.position.x - _offset);
}
