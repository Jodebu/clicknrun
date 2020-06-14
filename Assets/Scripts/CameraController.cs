using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private float playerOffset;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerOffset = transform.position.x - player.transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + playerOffset, transform.position.y, transform.position.z);
    }
}
