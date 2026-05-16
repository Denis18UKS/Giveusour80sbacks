using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 1f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;

    // Последнее направление, чтобы idle оставался повернут
    // в нужную сторону после остановки
    private string lastDirection = "down";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        HandleAnimation();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }

    void HandleInput()
    {
        movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            movement.y = 1;

        if (Keyboard.current.sKey.isPressed)
            movement.y = -1;

        if (Keyboard.current.aKey.isPressed)
            movement.x = -1;

        if (Keyboard.current.dKey.isPressed)
            movement.x = 1;

        movement = movement.normalized;
    }

    void HandleAnimation()
    {
        // Персонаж движется
        if (movement != Vector2.zero)
        {
            // Вверх (спиной к игроку)
            if (movement.y > 0)
            {
                lastDirection = "up";
                PlayAnimation("walk_bottom");
            }

            // Вниз (лицом к игроку)
            else if (movement.y < 0)
            {
                lastDirection = "down";
                PlayAnimation("walk");
            }

            // Влево
            else if (movement.x < 0)
            {
                lastDirection = "left";
                PlayAnimation("walk_left");
            }

            // Вправо
            else if (movement.x > 0)
            {
                lastDirection = "right";
                PlayAnimation("walk_right");
            }
        }

        // Персонаж стоит
        else
        {
            switch (lastDirection)
            {
                case "up":
                    PlayAnimation("idle_bottom");
                    break;

                case "down":
                    PlayAnimation("idle");
                    break;

                case "left":
                    PlayAnimation("idle_left");
                    break;

                case "right":
                    PlayAnimation("idle_right");
                    break;
            }
        }
    }

    void PlayAnimation(string animationName)
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (!state.IsName(animationName))
        {
            animator.Play(animationName);
        }
    }
}