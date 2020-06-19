using UnityEngine;
using UnityEngine.UI;

public class Scrollable : MonoBehaviour
{
    private void Update()
    {
        if (GameController.Instance.GameStatus == GameController.Status.STARTED)
            transform.position = new Vector3(transform.position.x, transform.position.y + Input.mouseScrollDelta.y * GameController.Instance.scrollSpeed, transform.position.z);
    }
}
