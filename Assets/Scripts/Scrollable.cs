using UnityEngine;
using UnityEngine.UI;

public class Scrollable : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Input.mouseScrollDelta.y * 0.5f, transform.position.z);
    }
}
