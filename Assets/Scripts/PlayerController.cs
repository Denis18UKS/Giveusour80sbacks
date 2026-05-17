using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 1f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;
    private string lastDirection = "down";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // ApplyModeSettings();
    }

    public void ApplyMode()
    {
        if (GameState.isPacmanMode)
        {
            speed = 3f;
            transform.localScale = new Vector3(2f, 2f, 2f);
        }
        else
        {
            speed = 1f;
            transform.localScale = Vector3.one;
        }
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
        if (movement != Vector2.zero)
        {
            if (movement.y > 0)
            {
                lastDirection = "up";
                PlayAnimation("walk_bottom");
            }
            else if (movement.y < 0)
            {
                lastDirection = "down";
                PlayAnimation("walk");
            }
            else if (movement.x < 0)
            {
                lastDirection = "left";
                PlayAnimation("walk_left");
            }
            else if (movement.x > 0)
            {
                lastDirection = "right";
                PlayAnimation("walk_right");
            }
        }
        else
        {
            switch (lastDirection)
            {
                case "up": PlayAnimation("idle_bottom"); break;
                case "down": PlayAnimation("idle"); break;
                case "left": PlayAnimation("idle_left"); break;
                case "right": PlayAnimation("idle_right"); break;
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