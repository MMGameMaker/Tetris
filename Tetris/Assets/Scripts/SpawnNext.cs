using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNext : MonoBehaviour
{
    [SerializeField]
    private SpawnTetromino spawner;
    private GameObject currentTetromino;
    private int currentTetrominoId;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateStoppedGroup();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTetrominoId != spawner.nextID)
        {
            DeleteBlockGroup();
            CreateStoppedGroup();
        }
    }

    private void CreateStoppedGroup()
    {
        currentTetromino = spawner.CreateBlock(transform.position);
        currentTetrominoId = spawner.nextID;

        TetrisBlock block = currentTetromino.GetComponent<TetrisBlock>();

        block.enabled = false;
    }

    private void DeleteBlockGroup()
    {
        Destroy(currentTetromino);
    }


}
