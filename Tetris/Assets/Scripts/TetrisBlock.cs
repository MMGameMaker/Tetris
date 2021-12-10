using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;

    private float lastFall;

    private float lastKeyDow;
    private float timeKeyPressed;

    

    // Start is called before the first frame update
    void Start()
    {
        lastFall = Time.time;
        lastKeyDow = Time.time;
        timeKeyPressed = Time.time;
        if (IsValidGridPos())
        {
            InsertOnGrid();
        }
        else
        {
            Debug.Log("Killed on Start");
            GameOver();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GetKey(KeyCode.LeftArrow))
        {
            TryChangePos(new Vector3(-1, 0, 0));
        }
        else if (GetKey(KeyCode.RightArrow))
        {
            TryChangePos(new Vector3(1, 0, 0));
        }
        else if (GetKey(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);

            if (IsValidGridPos())
            {
                UpdateGrid();
            }
            else
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        }
        else if (GetKey(KeyCode.DownArrow) || (Time.time - lastFall) >= 0.8f )
        {
            FallGroup();
        }



    }

    private bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = GridControl.RoundVector2(child.position);

            if (!GridControl.InsideBorder(v))
            {
                return false;
            }

            if (GridControl.grid[(int)v.x, (int)v.y] != null &&
                GridControl.grid[(int)v.x, (int)v.y].parent != transform)
            {
                return false;
            }
        }

        return true;
    }

    private void UpdateGrid()
    {
        //remove old children from grid
        for(int y = 0; y< GridControl.height; y++)
        {
            for(int x = 0; x< GridControl.width; x ++)
            {
                if(GridControl.grid[x,y] != null &&
                   GridControl.grid[x, y].parent == transform)
                {
                    GridControl.grid[x, y] = null;
                }
            }
        }

        InsertOnGrid();
    }

    private void InsertOnGrid()
    {
        // add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = GridControl.RoundVector2(child.position);
            GridControl.grid[(int)v.x, (int)v.y] = child;
        }
    }

    private void TryChangePos(Vector3 v)
    {
        //modify position
        transform.position += v;

        //check if valid
        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.position -= v;
        }
    }

    private void FallGroup()
    {
        transform.position += new Vector3(0, -1, 0);

        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);

            GridControl.DeleteFullRows();

            FindObjectOfType<SpawnTetromino>().SpawnNewBlock();


            //disable script
            enabled = false;
        }

        lastFall = Time.time;
    }

    private bool GetKey(KeyCode key)
    {
        bool keyDown = Input.GetKeyDown(key);
        bool pressed = Input.GetKey(key) && Time.time - lastKeyDow > 0.5f && Time.time - timeKeyPressed > 0.05f;

        if (keyDown)
        {
            lastKeyDow = Time.time;
        }

        if (pressed)
        {
            timeKeyPressed = Time.time;
        }

        return keyDown || pressed;
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER!");
    }

}
