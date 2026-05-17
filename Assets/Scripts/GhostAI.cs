using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    public string prefix = "rg";

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 currentDirection;
    private Vector2 nextDirection;

    private float checkDistance = 0.55f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentDirection = Vector2.right;
        nextDirection = currentDirection;
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        ChooseBestDirection();

        TryChangeDirection();

        rb.linearVelocity = currentDirection * speed;

        PlayAnimation(currentDirection);
    }

    void ChooseBestDirection()
    {
        Vector2[] directions =
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        float bestDistance = Mathf.Infinity;
        Vector2 bestDir = currentDirection;

        foreach (Vector2 dir in directions)
        {
            // нельзя идти назад
            if (dir == -currentDirection)
                continue;

            // проверяем стену
            if (IsWall(dir))
                continue;

            Vector2 nextPos =
                (Vector2)transform.position + dir;

            float dist =
                Vector2.Distance(nextPos, player.position);

            if (dist < bestDistance)
            {
                bestDistance = dist;
                bestDir = dir;
            }
        }

        nextDirection = bestDir;
    }

    void TryChangeDirection()
    {
        // если впереди стена → поворачиваем
        if (IsWall(currentDirection))
        {
            currentDirection = nextDirection;
            return;
        }

        // проверяем центр клетки
        Vector2 center =
            new Vector2(
                Mathf.Round(transform.position.x),
                Mathf.Round(transform.position.y)
            );

        float dist =
            Vector2.Distance(transform.position, center);

        // поворот только в центре клетки
        if (dist < 0.1f)
        {
            if (!IsWall(nextDirection))
            {
                currentDirection = nextDirection;
            }

            transform.position = center;
        }
    }

    bool IsWall(Vector2 dir)
    {
        Vector2 checkPos =
            (Vector2)transform.position +
            dir * checkDistance;

        Collider2D hit =
            Physics2D.OverlapCircle(checkPos, 0.2f);

        if (hit == null)
            return false;

        return hit.CompareTag("Wall");
    }

    void PlayAnimation(Vector2 dir)
    {
        if (dir == Vector2.right)
            anim.Play(prefix + "_right");

        else if (dir == Vector2.left)
            anim.Play(prefix + "_left");

        else if (dir == Vector2.up)
            anim.Play(prefix + "_top");

        else if (dir == Vector2.down)
            anim.Play(prefix + "_bottom");
    }
}