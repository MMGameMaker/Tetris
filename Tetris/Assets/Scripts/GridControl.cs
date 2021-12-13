using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    public static GridControl Instance;
    public static int width = GameSettings.Constants.BoardSizeX;
    public static int height = GameSettings.Constants.BoardSizeY;

    //grid storing the Transform element
    public static Transform[,] grid = new Transform[width, height];

    public int score;
    public int Score 
    { 
        get => this.score; 
        private set { this.score = value; } 
    }

    private bool isBusy;

    public bool IsBusy 
    { 
        get => isBusy;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        GameManager.Instance.OnGameStateChange += GameStateChangeHandler;

    }

    private void Start()
    {
        this.score = 0;
    }

    private void GameStateChangeHandler(GameManager.eGameStateS currentGameSate)
    {
        switch (currentGameSate)
        {
            case GameManager.eGameStateS.SETUP:
                Time.timeScale = 0;
                break;
            case GameManager.eGameStateS.PLAYING:
                Time.timeScale = 1;
                break;
            case GameManager.eGameStateS.PAUSING:
                Time.timeScale = 0;
                break;
            case GameManager.eGameStateS.GAMEOVER:
               
                break;
        }
    }

   

    public Vector2 RoundVector2(Vector2 piecePos)
    {
        return new Vector2(Mathf.Round(piecePos.x), Mathf.Round(piecePos.y));
    }

    //Check if some vector inside the game border (borders left, right and down)
    public bool InsideBorder(Vector2 pos)
    {
        if (pos.x < 0 || pos.x > width - 1 || pos.y < 0)
            return false;
        else
            return true;
    }

    public bool IsRowFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
                return false;
        }

        return true;
    }

    public void DeleteFullRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowAbove(y + 1);
                --y;
            }
        }
    }

    //destroy the row at y line
    public void DeleteRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }

        Score += width;

        EventDispatcher.Instance.PostEvent(GameSettings.EventID.ScoreChange, Score);
    }

    public void DecreaseRowAbove(int y)
    {
        for (int i = y; i < height; i++)
        {
            DecreaseRow(i);
        }
    }

    private void DecreaseRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].transform.position += new Vector3(0, -1, 0);
            }

        }
    }
}
