﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float SPAWN_DISTANCE = 25f;

    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform startBlock = null;
    [SerializeField] private List<Transform> blocks = new List<Transform>();

    private Vector3 lastEndPosition = Vector3.zero;
    private int lastSpawnedBlock = -2;
    private int maxHeigth;
    private int currentHeigth = 1;

    private void Awake()
    {
        maxHeigth = (int)Math.Floor(mainCamera.orthographicSize * 2 - 2);
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
        Transform transform = GetNextBlock();
        lastEndPosition = transform.Find("EndPosition").position;
    }

    private Transform GetNextBlock()
    {
        Transform nextBlock = GetBlock();
        Vector3 spawnPosition = GetPosition();
        

        Transform transform = Instantiate(nextBlock, spawnPosition, Quaternion.identity);
        return transform;

        Transform GetBlock()
        {
            int newBlock;

            do newBlock = UnityEngine.Random.Range(0, blocks.Count);
            while (newBlock == lastSpawnedBlock);

            lastSpawnedBlock = newBlock;
            return blocks[newBlock];
        }

        Vector3 GetPosition()
        {
            int nextHeigthIncrement = 
                nextBlock.gameObject.GetComponent<Scrollable>() != null
                || blocks[lastSpawnedBlock].gameObject.GetComponent<Scrollable>() != null
                    ? UnityEngine.Random.Range(1 - currentHeigth, maxHeigth - currentHeigth)
                    : UnityEngine.Random.Range(currentHeigth <= 1 ? 0 : -1, currentHeigth >= maxHeigth ? 1 : 2);

            currentHeigth += nextHeigthIncrement;

            return new Vector3(
                lastEndPosition.x + UnityEngine.Random.Range(0, 3),
                lastEndPosition.y + nextHeigthIncrement,
                lastEndPosition.z);
        }
    }
}
