using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{

    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Vector2Int gridDimensions = new Vector2Int(5, 4);
    [SerializeField] private Vector2 cellSize = new Vector2(0.8f, 0.15f);
    [SerializeField] private Vector2 offset = new Vector2(0.5f, 0.5f);


    private void Start()
    {
        // Calculate the starting position for spawning bricks
        Vector2 startPos = (Vector2)transform.position - new Vector2(gridDimensions.x * cellSize.x, gridDimensions.y * cellSize.y) * 0.5f + offset;

        // Loop through each cell in the grid
        for (int y = 0; y < gridDimensions.y; y++)
        {
            for (int x = 0; x < gridDimensions.x; x++)
            {
                // Calculate the position for this brick
                Vector2 spawnPos = startPos + new Vector2(x * cellSize.x, y * cellSize.y);

                // Spawn the brick at the calculated position
                GameObject brick = Instantiate(brickPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

}