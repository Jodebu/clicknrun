using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Transform player = null;

    private TextMeshProUGUI score;
    private float offset;

    private void Awake()
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
