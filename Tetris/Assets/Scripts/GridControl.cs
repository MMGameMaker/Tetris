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

    private int rowDeleting;

    public int RowDeleting { get => rowDeleting; }

    private float fallDuration;
    public float FallDuration
    {
        get => fallDuration;
        set { fallDuration = value; }
    }

    [SerializeField]
    SpawnTetromino spawner;

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
        StartNewGame();
    }

    public void StartNewGame()
    {
        this.score = 0;
        rowDeleting = 0;
        FallDuration = GameSettings.Constants.SlowestDuration;
        spawner.StartGame();
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
                Time.timeScale = 0;
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

    public IEnumerator DeleteFullRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsRowFull(y))
            {
                StartCoroutine(DeleteRow(y));
                yield return new WaitWhile(() => IsBusy);
                DecreaseRowAbove(y + 1);
                --y;
            }
        }
    }

    //destroy the row at y line
    public IEnumerator DeleteRow(int y)
    {
        this.isBusy = true;
        this.rowDeleting++;

        for (int x = 0; x < width/2; x++)
        {
            Destroy(grid[width / 2 - 1 - x, y].gameObject);
            Destroy(grid[width / 2 + x, y].gameObject);
            grid[width / 2 - 1 - x, y] = null;
            grid[width / 2 + x, y] = null;

            yield return new WaitForSeconds(0.05f);
        }

        this.isBusy = false;
        this.rowDeleting--;

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

    public void ClearAll()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                    grid[x, y] = null;
                }
            }
        }
    }
}
