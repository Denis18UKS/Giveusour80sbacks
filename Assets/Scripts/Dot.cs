using UnityEngine;

public class Dot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.AddScore(1);
            Destroy(gameObject);
        }
    }
}