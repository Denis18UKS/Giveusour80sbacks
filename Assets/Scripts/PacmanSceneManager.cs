using System.Collections;
using UnityEngine;

public class PacmanSceneManager : MonoBehaviour
{
    public Transform spawnPoint;

    IEnumerator Start()
    {
        // ждём загрузку сцены
        yield return null;

        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // размер
            player.transform.localScale =
                new Vector3(3f, 3f, 3f);

            // позиция
            player.transform.position =
                spawnPoint.position;

            // скорость
            PlayerController controller =
                player.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.speed = 3f;

                Debug.Log("PACMAN SPEED = " + controller.speed);
            }
            else
            {
                Debug.LogError("PlayerController NOT FOUND");
            }
        }
        else
        {
            Debug.LogError("PLAYER NOT FOUND");
        }
    }
}