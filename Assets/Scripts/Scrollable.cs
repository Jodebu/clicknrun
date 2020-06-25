using UnityEngine;

public class Scrollable : MonoBehaviour
{
    private void Update()
    {
        if (GameController.Instance.GameStatus != GameController.Status.Started) return;
        var position = transform.position;
        position = new Vector3(position.x, position.y + Input.mouseScrollDelta.y * GameController.Instance.ScrollSpeed, position.z);
        transform.position = position;
    }
}
