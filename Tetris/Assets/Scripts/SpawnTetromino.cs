using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    public GameObject[] Tetrominoes;
    public int nextID;

    // Start is called before the first frame update
    void Start()
    {
        GetnextID();
        SpawnNewBlock();
    }

    private int GetnextID()
    {
        nextID = Random.Range(0, Tetrominoes.Length);
        return nextID;
    }

    // Update is called once per frame
    public void SpawnNewBlock()
    {
        CreateBlock(this.transform.position);

        GetnextID();
    }

    public GameObject CreateBlock(Vector3 pos)
    {
        GameObject tetromino = Instantiate(Tetrominoes[nextID], pos, Quaternion.identity);
        return tetromino;
    }

}
