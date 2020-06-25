using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
    {
        if (GameController.Instance.GameStatus == GameController.Status.Started)
            transform.Translate(GameController.Instance.Speed * Time.deltaTime, 0, 0);
    }
}
