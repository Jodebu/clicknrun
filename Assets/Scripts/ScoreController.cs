using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Transform player = null;

    private TextMeshProUGUI score;
    private float offset;

    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        offset = player.position.x;
    }

    private void Update()
    {
        score.text = GetDistanceTraveled().ToString();
    }

    private int GetDistanceTraveled() => (int)(player.position.x - offset);
}
