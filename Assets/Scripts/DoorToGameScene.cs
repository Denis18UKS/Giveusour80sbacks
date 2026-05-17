using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorToGameScene : MonoBehaviour
{
    private bool playerInRange = false;

    void Update()
    {
        if (!playerInRange) return;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Переход в GameScene");

            SceneManager.LoadScene("GameScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Подошёл к двери. Нажми E");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Отошёл от двери");
        }
    }
}