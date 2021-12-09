using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    public float previousTime;
    public float fallTime = 0.5f;
    public static int height = 20;
    public static int width = 12;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove())
                this.transform.position += new Vector3(1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                this.transform.position += new Vector3(-1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if(!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            this.transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                this.transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().SpawnNewBlock();
            }

            previousTime = Time.time;
        }
    }

    private void CheckForLines()
    {
        for(int i = height-1; i >=0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    private bool HasLine(int i)
    {
        for(int j =0; j<width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);

            grid[i, j] = null;
        }
    }

    private void RowDown(int i)
    {
        for(int y = i; y<height-1; y++)
        {
            for(int j = 0; j <width; j++)
            {
                if(grid[j, y+1] != null)
                {
                    grid[j, y] = grid[j, y+1];
                    grid[j, y+1] = null;
                    grid[j, y].transform.position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    private void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x + 9.5f);
            int roundedY = Mathf.RoundToInt(children.transform.position.y + 9.5f);

            grid[roundedX, roundedY] = children;

            Debug.Log(roundedX + " , " + roundedY);
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x + 9.5f);
            int roundedY = Mathf.RoundToInt(children.transform.position.y + 9.5f);

            if (roundedX<0 || roundedX > width - 1 || roundedY < 0 || roundedY > height - 1)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }


}
