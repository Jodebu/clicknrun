using TMPro;
using UnityEngine;

public class ClickToBreak : MonoBehaviour
{
    [SerializeField] private TextMeshPro countdown = null;
    private int clicksLeft;

    private void Awake()
    {
        clicksLeft = Random.Range(1, 4);
        countdown.text = clicksLeft.ToString();
    }

    private void OnMouseDown()
    {
        countdown.text = (--clicksLeft).ToString();
        if (clicksLeft <= 0)
            Destroy(gameObject);
    }
}
