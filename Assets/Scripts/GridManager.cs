using UnityEngine;

public class GridManager : MonoBehaviour
{
    public const int width = 10;
    public const int height = 20;
    public const float cellSize = 0.45f;

    private static readonly Vector3 boardOrigin = new Vector3(-2.025f, -4.275f, 0f);

    public static Transform[,] grid = new Transform[width, height];

    void Awake()
    {
        ResetGrid();
    }

    public static void ResetGrid()
    {
        grid = new Transform[width, height];
    }

    public static Vector2Int WorldToCell(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - boardOrigin;

        return new Vector2Int(
            Mathf.RoundToInt(localPosition.x / cellSize),
            Mathf.RoundToInt(localPosition.y / cellSize)
        );
    }

    public static Vector3 CellToWorld(Vector2Int cell)
    {
        return boardOrigin + new Vector3(cell.x * cellSize, cell.y * cellSize, 0f);
    }

    public static bool IsInsideHorizontalBounds(Vector2Int cell)
    {
        return cell.x >= 0 && cell.x < width && cell.y >= 0;
    }

    public static bool IsOccupied(Vector2Int cell)
    {
        if (cell.y >= height)
            return false;

        return grid[cell.x, cell.y] != null;
    }

    public static void SetCell(Vector2Int cell, Transform block)
    {
        grid[cell.x, cell.y] = block;
        block.position = CellToWorld(cell);
    }

    public static int DeleteCompleteLines()
    {
        int deletedLines = 0;

        for (int y = 0; y < height; y++)
        {
            if (!IsFull(y))
                continue;

            DeleteRow(y);
            MoveRowsDown(y + 1);
            deletedLines++;
            y--;
        }

        return deletedLines;
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
            if (grid[x, y] != null)
                Object.Destroy(grid[x, y].gameObject);

            grid[x, y] = null;
        }
    }

    static void MoveRowsDown(int startY)
    {
        for (int y = startY; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Transform block = grid[x, y];

                if (block == null)
                    continue;

                grid[x, y - 1] = block;
                grid[x, y] = null;
                block.position = CellToWorld(new Vector2Int(x, y - 1));
            }
        }
    }
}
