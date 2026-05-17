using UnityEngine;
using UnityEngine.InputSystem;

public class Tetromino : MonoBehaviour
{
    public float speed = 2f;

    private bool activePiece = true;

    void Update()
    {
        // только активная фигура управляется
        if (!activePiece) return;

        HandleInput();

        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    void HandleInput()
    {
        var kb = Keyboard.current;

        if (kb == null) return;

        if (kb.leftArrowKey.wasPressedThisFrame)
            transform.position += Vector3.left;

        if (kb.rightArrowKey.wasPressedThisFrame)
            transform.position += Vector3.right;

        if (kb.downArrowKey.wasPressedThisFrame)
            transform.position += Vector3.down;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // фигура больше НЕ активна
        activePiece = false;

        // отключаем этот скрипт
        enabled = false;

        // создаём следующую фигуру
        FindFirstObjectByType<Spawner>().Spawn();
    }
}