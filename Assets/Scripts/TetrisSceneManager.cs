using UnityEngine;

public class TetrisSceneManager : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found in TetrisScene");
            return;
        }

        // ❌ выключаем управление
        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // ❌ выключаем физику (чтобы не дёргался)
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // ❌ фиксируем анимацию (если есть Animator)
        Animator anim = player.GetComponent<Animator>();
        if (anim != null)
        {
            anim.enabled = false;
        }

        Debug.Log("Player locked in TetrisScene (idle state)");
    }
}