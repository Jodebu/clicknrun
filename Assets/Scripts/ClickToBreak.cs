using TMPro;
using UnityEngine;

public class ClickToBreak : MonoBehaviour
{
    [SerializeField] private TextMeshPro countdown = null;
    private int _clicksLeft;

    private void Awake()
    {
        _clicksLeft = Random.Range(1, 4);
        countdown.text = _clicksLeft.ToString();
    }

    private void OnMouseDown()
    {
        countdown.text = (--_clicksLeft).ToString();
        if (_clicksLeft <= 0)
            Destroy(gameObject);
    }
}
