using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float SpawnDistance = 25f;

    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform startBlock = null;
    [SerializeField] private List<Transform> blocks = new List<Transform>();

    private Vector3 _lastEndPosition = Vector3.zero;
    private int _lastSpawnedBlock = -2;
    private int _maxHeight;
    private int _currentHeight = 1;

    private void Awake()
    {
        _maxHeight = (int)Math.Floor(mainCamera.orthographicSize * 2 - 2);
        _lastEndPosition = startBlock.Find("EndPosition").position;
        SpawnBlock();
    }

    private void Update()
    {
        if (player != null && Vector3.Distance(player.transform.position, _lastEndPosition) < SpawnDistance)
            SpawnBlock();
    }

    private void SpawnBlock()
    {
        Transform nextBlock = GetNextBlock();
        _lastEndPosition = nextBlock.Find("EndPosition").position;
    }

    private Transform GetNextBlock()
    {
        Transform nextBlock = GetBlock();
        Vector3 spawnPosition = GetPosition();
        

        Transform block = Instantiate(nextBlock, spawnPosition, Quaternion.identity);
        return block;

        Transform GetBlock()
        {
            int newBlock;

            do newBlock = UnityEngine.Random.Range(0, blocks.Count);
            while (newBlock == _lastSpawnedBlock);

            _lastSpawnedBlock = newBlock;
            return blocks[newBlock];
        }

        Vector3 GetPosition()
        {
            int nextHeightIncrement = 
                nextBlock.gameObject.GetComponent<Scrollable>() != null
                || blocks[_lastSpawnedBlock].gameObject.GetComponent<Scrollable>() != null
                    ? UnityEngine.Random.Range(1 - _currentHeight, _maxHeight - _currentHeight)
                    : UnityEngine.Random.Range(_currentHeight <= 1 ? 0 : -1, _currentHeight >= _maxHeight ? 1 : 2);

            _currentHeight += nextHeightIncrement;

            return new Vector3(
                _lastEndPosition.x + UnityEngine.Random.Range(0, 3),
                _lastEndPosition.y + nextHeightIncrement,
                _lastEndPosition.z);
        }
    }
}
