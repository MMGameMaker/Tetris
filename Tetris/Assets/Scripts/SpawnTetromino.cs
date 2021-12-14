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

    }

    public void StartGame()
    {
        if (transform.childCount > 0)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform t in transform)
            {
                children.Add(t.gameObject);
            }

            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }

        GetnextID();
        StartCoroutine("SpawnNewBlock");
    }

    private int GetnextID()
    {
        nextID = Random.Range(0, Tetrominoes.Length);
        return nextID;
    }

    // Update is called once per frame
    public IEnumerator SpawnNewBlock()
    {
        yield return new WaitWhile(() => GridControl.Instance.RowDeleting != 0);

        CreateBlock(this.transform.position);

        GetnextID();
    }

    public GameObject CreateBlock(Vector3 pos)
    {
        GameObject tetromino = Instantiate(Tetrominoes[nextID], pos, Quaternion.identity);
        tetromino.transform.parent = this.transform;
        return tetromino;
    }

}
