using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;

    public static Transform[,] grid = new Transform[10, 20];

    public static Vector2 Round(Vector2 pos)
    {
        return new Vector2(
            Mathf.Round(pos.x),
            Mathf.Round(pos.y)
        );
    }

    public static bool Inside(Vector2 pos)
    {
        return (int)pos.x >= 0 &&
               (int)pos.x < width &&
               (int)pos.y >= 0;
    }

    public static void DeleteLines()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsFull(y))
            {
                DeleteRow(y);
                MoveAllRowsDown(y + 1);

                y--;
            }
        }
    }

    static bool IsFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
                return false;
        }

        return true;
    }

    static void DeleteRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);

            grid[x, y] = null;
        }
    }

    static void MoveAllRowsDown(int startY)
    {
        for (int y = startY; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;

                    grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }

    public static bool CheckGameOver()
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, height - 1] != null)
            {
                TetrisGameManager.instance.GameOver();
                return true;
            }
        }

        return false;
    }
}