using UnityEngine;
using UnityEngine.InputSystem;

public class Tetromino : MonoBehaviour
{
    public float fallDelay = 1f;
    public float softDropDelay = 0.05f;
    public float lockDelay = 0.35f;

    private static Sprite blockSprite;

    private Vector2Int[] localCells;
    private Transform[] blocks;
    private Vector2Int boardPosition;
    private int rotation;
    private float fallTimer;
    private float softDropTimer;
    private float lockTimer;
    private bool canRotate;
    private bool isInitialized;
    private bool isLocked;

    public void Initialize(Vector2Int[] shape, Color color, bool rotatable, Vector2Int spawnCell)
    {
        localCells = (Vector2Int[])shape.Clone();
        blocks = new Transform[localCells.Length];
        boardPosition = spawnCell;
        canRotate = rotatable;

        for (int i = 0; i < localCells.Length; i++)
        {
            GameObject blockObject = new GameObject("Block");
            blockObject.transform.SetParent(transform, false);
            blockObject.transform.localScale = Vector3.one * (GridManager.cellSize * 0.94f);

            SpriteRenderer renderer = blockObject.AddComponent<SpriteRenderer>();
            renderer.sprite = GetBlockSprite();
            renderer.color = color;
            renderer.sortingOrder = 10;

            blocks[i] = blockObject.transform;
        }

        isInitialized = true;
        UpdateVisualPosition();
    }

    void Update()
    {
        if (!isInitialized || isLocked || TetrisGameManager.instance == null ||
            TetrisGameManager.instance.isGameOver)
        {
            return;
        }

        HandleInput();

        if (isLocked)
            return;

        HandleAutomaticFall();
        HandleLockDelay();
    }

    void HandleInput()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        if (keyboard.leftArrowKey.wasPressedThisFrame || keyboard.aKey.wasPressedThisFrame)
            TryMove(Vector2Int.left);

        if (keyboard.rightArrowKey.wasPressedThisFrame || keyboard.dKey.wasPressedThisFrame)
            TryMove(Vector2Int.right);

        if (keyboard.upArrowKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame ||
            keyboard.xKey.wasPressedThisFrame)
        {
            TryRotate(1);
        }

        if (keyboard.zKey.wasPressedThisFrame)
            TryRotate(-1);

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            while (TryMove(Vector2Int.down))
            {
            }

            Lock();
            return;
        }

        bool softDropPressed = keyboard.downArrowKey.isPressed || keyboard.sKey.isPressed;

        if (!softDropPressed)
        {
            softDropTimer = 0f;
            return;
        }

        softDropTimer += Time.deltaTime;

        if (keyboard.downArrowKey.wasPressedThisFrame || keyboard.sKey.wasPressedThisFrame ||
            softDropTimer >= softDropDelay)
        {
            TryMove(Vector2Int.down);
            softDropTimer = 0f;
            fallTimer = 0f;
        }
    }

    void HandleAutomaticFall()
    {
        fallTimer += Time.deltaTime;

        if (fallTimer < fallDelay)
            return;

        TryMove(Vector2Int.down);
        fallTimer = 0f;
    }

    void HandleLockDelay()
    {
        if (CanOccupy(boardPosition + Vector2Int.down, rotation))
        {
            lockTimer = 0f;
            return;
        }

        lockTimer += Time.deltaTime;

        if (lockTimer >= lockDelay)
            Lock();
    }

    bool TryMove(Vector2Int direction)
    {
        Vector2Int nextPosition = boardPosition + direction;

        if (!CanOccupy(nextPosition, rotation))
            return false;

        boardPosition = nextPosition;
        lockTimer = 0f;
        UpdateVisualPosition();
        return true;
    }

    void TryRotate(int direction)
    {
        if (!canRotate)
            return;

        int nextRotation = (rotation + direction + 4) % 4;
        Vector2Int[] kicks =
        {
            Vector2Int.zero,
            Vector2Int.left,
            Vector2Int.right,
            new Vector2Int(-2, 0),
            new Vector2Int(2, 0),
            Vector2Int.up
        };

        foreach (Vector2Int kick in kicks)
        {
            Vector2Int kickedPosition = boardPosition + kick;

            if (!CanOccupy(kickedPosition, nextRotation))
                continue;

            boardPosition = kickedPosition;
            rotation = nextRotation;
            lockTimer = 0f;
            UpdateVisualPosition();
            return;
        }
    }

    bool CanOccupy(Vector2Int position, int targetRotation)
    {
        for (int i = 0; i < localCells.Length; i++)
        {
            Vector2Int cell = position + RotateCell(localCells[i], targetRotation);

            if (!GridManager.IsInsideHorizontalBounds(cell) || GridManager.IsOccupied(cell))
                return false;
        }

        return true;
    }

    public bool IsInValidPosition()
    {
        return isInitialized && CanOccupy(boardPosition, rotation);
    }

    void UpdateVisualPosition()
    {
        transform.position = GridManager.CellToWorld(boardPosition);

        for (int i = 0; i < blocks.Length; i++)
        {
            Vector2Int cell = RotateCell(localCells[i], rotation);
            blocks[i].localPosition = new Vector3(
                cell.x * GridManager.cellSize,
                cell.y * GridManager.cellSize,
                0f
            );
        }
    }

    void Lock()
    {
        if (isLocked)
            return;

        isLocked = true;
        enabled = false;

        Vector2Int[] occupiedCells = new Vector2Int[localCells.Length];

        for (int i = 0; i < localCells.Length; i++)
        {
            occupiedCells[i] = boardPosition + RotateCell(localCells[i], rotation);

            if (occupiedCells[i].y >= GridManager.height)
            {
                TetrisGameManager.instance.GameOver();
                Destroy(gameObject);
                return;
            }
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            Transform block = blocks[i];
            block.SetParent(null, true);
            block.name = "LockedBlock";
            GridManager.SetCell(occupiedCells[i], block);
        }

        int deletedLines = GridManager.DeleteCompleteLines();

        TetrisGameManager.instance.PieceLocked(deletedLines);
        Destroy(gameObject);
    }

    static Vector2Int RotateCell(Vector2Int cell, int turnsClockwise)
    {
        Vector2Int rotated = cell;

        for (int i = 0; i < turnsClockwise; i++)
            rotated = new Vector2Int(rotated.y, -rotated.x);

        return rotated;
    }

    static Sprite GetBlockSprite()
    {
        if (blockSprite != null)
            return blockSprite;

        const int textureSize = 10;
        Texture2D texture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        texture.name = "RuntimeTetrisBlock";
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        for (int y = 0; y < textureSize; y++)
        {
            for (int x = 0; x < textureSize; x++)
            {
                bool border = x == 0 || y == 0 || x == textureSize - 1 || y == textureSize - 1;
                texture.SetPixel(x, y, border ? new Color(0.12f, 0.12f, 0.18f, 1f) : Color.white);
            }
        }

        texture.Apply();
        blockSprite = Sprite.Create(
            texture,
            new Rect(0f, 0f, textureSize, textureSize),
            new Vector2(0.5f, 0.5f),
            textureSize
        );
        blockSprite.name = "RuntimeTetrisBlock";
        return blockSprite;
    }
}
