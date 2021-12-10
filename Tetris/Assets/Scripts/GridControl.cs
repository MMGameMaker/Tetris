using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    public static int width = 12;
    public static int height = 20;

    //grid storing the Transform element
    public static Transform[,] grid = new Transform[width, height];

    public static Vector2 RoundVector2(Vector2 piecePos)
    {
        return new Vector2(Mathf.Round(piecePos.x), Mathf.Round(piecePos.y));
    }

    //Check if some vector inside the game border (borders left, right and down)
    public static bool InsideBorder(Vector2 pos)
    {
        if (pos.x < 0 || pos.x > width -1 || pos.y < 0)
            return false;
        else
            return true;
    }

    public static bool IsRowFull(int y)
    {
        for(int x = 0; x<width; x++)
        {
            if (grid[x, y] == null)
                return false;
        }

        return true;
    }

    public static void DeleteFullRows()
    {
        for(int y =0; y<height; y++)
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
    public static void DeleteRow(int y)
    {
        for(int x = 0; x< width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void DecreaseRowAbove(int y)
    {
        for(int i = y; i < height; i++)
        {
            DecreaseRow(i);
        }
    }

    private static void DecreaseRow(int y)
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
