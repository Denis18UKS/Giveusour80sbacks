using UnityEngine;
using UnityEngine.InputSystem;

public class Tetromino : MonoBehaviour
{
    private float fallTimer;

    public float fallDelay = 1f;

    private static int width = 10;
    private static int height = 20;

    private static Transform[,] grid = new Transform[10, 20];

    void Update()
    {
        if (TetrisGameManager.instance.isGameOver)
            return;

        HandleInput();

        HandleFall();
    }

    void HandleInput()
    {
        var keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        if (keyboard.leftArrowKey.wasPressedThisFrame)
        {
            Move(Vector3.left);
        }

        if (keyboard.rightArrowKey.wasPressedThisFrame)
        {
            Move(Vector3.right);
        }

        if (keyboard.downArrowKey.wasPressedThisFrame)
        {
            Move(Vector3.down);
        }
    }

    void HandleFall()
    {
        fallTimer += Time.deltaTime;

        if (fallTimer >= fallDelay)
        {
            Move(Vector3.down);

            fallTimer = 0;
        }
    }

    void Move(Vector3 dir)
    {
        transform.position += dir;

        if (!ValidPosition())
        {
            transform.position -= dir;

            if (dir == Vector3.down)
            {
                Lock();
            }
        }
    }

    bool ValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = Round(child.position);

            if (!InsideGrid(pos))
            {
                return false;
            }

            if (pos.y < height)
            {
                if (grid[(int)pos.x, (int)pos.y] != null &&
                    grid[(int)pos.x, (int)pos.y].parent != transform)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void Lock()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = Round(child.position);

            int x = (int)pos.x;
            int y = (int)pos.y;

            // защита от выхода за границы
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                TetrisGameManager.instance.GameOver();
                return;
            }

            grid[x, y] = child;
        }

        enabled = false;

        TetrisGameManager.instance.PieceLocked();
    }

    bool InsideGrid(Vector2 pos)
    {
        return (int)pos.x >= 0 &&
               (int)pos.x < width &&
               (int)pos.y >= 0;
    }

    Vector2 Round(Vector3 pos)
    {
        return new Vector2(
            Mathf.Round(pos.x),
            Mathf.Round(pos.y)
        );
    }
}