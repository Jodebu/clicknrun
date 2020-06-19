using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        if (GameController.Instance.GameStatus == GameController.Status.STARTED)
            transform.Translate(GameController.Instance.speed * Time.deltaTime, 0, 0);
    }
}
