using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float SPAWN_DISTANCE = 25f;
    private const int MAX_HEIGTH = 8;

    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform startBlock = null;
    [SerializeField] private List<Transform> blocks = new List<Transform>();

    private Vector3 lastEndPosition = Vector3.zero;
    private int heigth = 1;

    private void Awake()
    {
        lastEndPosition = startBlock.Find("EndPosition").position;
        SpawnBlock();
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < SPAWN_DISTANCE)
        {
            SpawnBlock();
        }
    }

    private void SpawnBlock()
    {
        Transform transform = SpawnBlock(GetNextBlock(), lastEndPosition);
        lastEndPosition = transform.Find("EndPosition").position;
    }

    private Transform GetNextBlock()
    {
        return blocks[Random.Range(0, blocks.Count)];
    }

    private Transform SpawnBlock(Transform block, Vector3 spawnPosition)
    {
        Transform transform = Instantiate(block, spawnPosition, Quaternion.identity);
        return transform;
    }
}
