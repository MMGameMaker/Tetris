using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eInputControl
{
    None,
    Rotate,
    LeftMove,
    RightMove,
    PushDown,
}


public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;

    private float lastFall;
    private float lastKeyDow;
    private float timeKeyPressed;

    private eInputControl inputdirection;
    private bool isPushDown;
    private bool couldRotate;

    private Touch touch;
    private Vector2 startTouchPos;
    private Vector2 lastMoveTouchPos;
    private Vector2 endTouchPos;
    private Vector2 touchDirection;

    

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

        inputdirection = eInputControl.None;
        isPushDown = false;
        couldRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentGameStates != GameManager.eGameStateS.PLAYING)
            return;

        inputdirection = GetTouch();

        if (GetKey(KeyCode.LeftArrow) || inputdirection == eInputControl.LeftMove)
        {
            TryChangePos(new Vector3(-1, 0, 0));
        }
        else if (GetKey(KeyCode.RightArrow) || inputdirection == eInputControl.RightMove)
        {
            TryChangePos(new Vector3(1, 0, 0));
        }
        else if (GetKey(KeyCode.UpArrow) || inputdirection == eInputControl.Rotate)
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

        if(Time.time - lastFall >= ((GetKey(KeyCode.DownArrow) || isPushDown) ?
            GameSettings.Constants.FastestDuration : GridControl.Instance.FallDuration))
        {
            FallGroup();
        }
    }

    private eInputControl GetTouch()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (isPushDown)
                {
                    isPushDown = false;
                    couldRotate = false;
                }
                    

                startTouchPos = touch.position;
                lastMoveTouchPos = touch.position;
                lastKeyDow = Time.time;
            }
            else if (touch.phase == TouchPhase.Moved && !isPushDown)
            {
                touchDirection = touch.position - lastMoveTouchPos;
                float xDir = touchDirection.x;
                float yDir = touchDirection.y;

                if (Mathf.Abs(xDir) >= 50)
                {
                    lastMoveTouchPos = touch.position;
                    if (xDir > 0)
                        return eInputControl.RightMove;
                    else if (xDir < 0)
                        return eInputControl.LeftMove;
                }
                else if (yDir <= - 80f)
                {
                    isPushDown = true;
                    return eInputControl.PushDown;
                }
                 
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPos = touch.position;
                touchDirection = endTouchPos - startTouchPos;

                if (Time.time - lastKeyDow <= 0.2f && touchDirection.magnitude <= 45)
                {
                    if (couldRotate)
                        return eInputControl.Rotate;
                    else
                        couldRotate = true;
                }
                Debug.Log(endTouchPos);
            }

            //boardController.IsGetTouchInput = false;
        }
        else if (inputdirection == eInputControl.PushDown)
            return eInputControl.PushDown;
     
        return eInputControl.None;
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

    private bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = GridControl.Instance.RoundVector2(child.position);

            if (!GridControl.Instance.InsideBorder(v))
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
            Vector2 v = GridControl.Instance.RoundVector2(child.position);
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

            GridControl.Instance.DeleteFullRows();

            FindObjectOfType<SpawnTetromino>().SpawnNewBlock();

            //disable script
            enabled = false;
        }

        lastFall = Time.time;
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER!");
        GameManager.Instance.CurrentGameStates = GameManager.eGameStateS.GAMEOVER;
    }

}
