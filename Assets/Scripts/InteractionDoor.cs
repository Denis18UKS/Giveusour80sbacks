using UnityEngine;
using UnityEngine.InputSystem;

public class DoorTrigger : MonoBehaviour
{
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Игрок вошёл в дверь!");
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