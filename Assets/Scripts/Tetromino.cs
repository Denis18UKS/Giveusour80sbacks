using UnityEngine;
using UnityEngine.InputSystem;

public class Tetromino : MonoBehaviour
{
    public float fallTime = 1f;
    private float lastFall;

    private static int width = 10;
    private static int height = 20;

    private static Transform[,] grid = new Transform[10, 20];

    private Spawner spawner;

    void Start()
    {
        lastFall = Time.time;
        spawner = FindFirstObjectByType<Spawner>();
    }

    void Update()
    {
        HandleInput();
        HandleFall();
    }

    void HandleInput()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        if (kb.leftArrowKey.wasPressedThisFrame)
            Move(Vector3.left);

        if (kb.rightArrowKey.wasPressedThisFrame)
            Move(Vector3.right);

        if (kb.downArrowKey.wasPressedThisFrame)
            Move(Vector3.down);
    }

    void HandleFall()
    {
        if (Time.time - lastFall >= fallTime)
        {
            Move(Vector3.down);
            lastFall = Time.time;
        }
    }

    void Move(Vector3 dir)
    {
        transform.position += dir;

        if (Valid() == false)
        {
            transform.position -= dir;

            if (dir == Vector3.down)
            {
                Lock();
            }
        }
    }

    bool Valid()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = Round(child.position);

            if (!Inside(pos))
                return false;

            if (grid[(int)pos.x, (int)pos.y] != null)
                return false;
        }

        return true;
    }

    bool Inside(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < width &&
               pos.y >= 0 && pos.y < height;
    }

    Vector2 Round(Vector3 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    void Lock()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = Round(child.position);

            if (Inside(pos))
                grid[(int)pos.x, (int)pos.y] = child;
        }

        if (spawner != null)
            spawner.Spawn();

        Destroy(gameObject);
    }
}