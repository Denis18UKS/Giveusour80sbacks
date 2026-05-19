using UnityEngine;
using UnityEngine.InputSystem;

public class Tetromino : MonoBehaviour
{
    private float fallTimer;

    public float fallDelay = 1f;

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

        // ROTATE
        if (keyboard.upArrowKey.wasPressedThisFrame)
        {
            Rotate();
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

    void Rotate()
    {
        transform.Rotate(0, 0, -90);

        if (!ValidPosition())
        {
            transform.Rotate(0, 0, 90);
        }
    }

    bool ValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = GridManager.Round(child.position);

            if (!GridManager.Inside(pos))
            {
                return false;
            }

            if (pos.y < GridManager.height)
            {
                if (GridManager.grid[(int)pos.x, (int)pos.y] != null &&
                    GridManager.grid[(int)pos.x, (int)pos.y].parent != transform)
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
            Vector2 pos = GridManager.Round(child.position);

            int x = (int)pos.x;
            int y = (int)pos.y;

            // защита от выхода за границы
            if (x < 0 || x >= GridManager.width ||
                y < 0 || y >= GridManager.height)
            {
                TetrisGameManager.instance.GameOver();
                return;
            }

            GridManager.grid[x, y] = child;
        }

        GridManager.DeleteLines();

        GridManager.CheckGameOver();

        enabled = false;

        TetrisGameManager.instance.PieceLocked();
    }
}