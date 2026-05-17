using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    public string prefix = "rg";

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        direction = Vector2.left;
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }

        MoveAI();

        rb.linearVelocity = direction * speed;

        PlayAnim();
    }

    void FindPlayer()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void MoveAI()
    {
        // если упёрся в стену → меняем направление
        if (IsWall(direction))
        {
            ChooseNewDirection();
        }

        // на “перекрёстках” тоже меняем
        if (AtIntersection())
        {
            ChooseNewDirection();
        }
    }

    void ChooseNewDirection()
    {
        Vector2[] dirs = {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        Vector2 bestDir = direction;
        float bestDist = Mathf.Infinity;

        foreach (var dir in dirs)
        {
            if (dir == -direction) continue;

            if (IsWall(dir)) continue;

            Vector2 nextPos =
                (Vector2)transform.position + dir;

            float dist =
                Vector2.Distance(nextPos, player.position);

            if (dist < bestDist)
            {
                bestDist = dist;
                bestDir = dir;
            }
        }

        direction = bestDir;
    }

    bool AtIntersection()
    {
        int openPaths = 0;

        Vector2[] dirs = {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        foreach (var dir in dirs)
        {
            if (!IsWall(dir))
                openPaths++;
        }

        return openPaths >= 3;
    }

    bool IsWall(Vector2 dir)
    {
        RaycastHit2D hit =
            Physics2D.Raycast(
                transform.position,
                dir,
                0.6f
            );

        return hit.collider != null &&
               hit.collider.CompareTag("Wall");
    }

    void PlayAnim()
    {
        if (direction == Vector2.right)
            anim.Play(prefix + "_right");

        else if (direction == Vector2.left)
            anim.Play(prefix + "_left");

        else if (direction == Vector2.up)
            anim.Play(prefix + "_top");

        else if (direction == Vector2.down)
            anim.Play(prefix + "_bottom");
    }
}