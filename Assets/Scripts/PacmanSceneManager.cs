using UnityEngine;

public class PacmanSceneManager : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Увеличение размера
            player.transform.localScale = new Vector3(2f, 2f, 2f);

            // Перемещение на spawn
            player.transform.position = spawnPoint.position;

            // Увеличение скорости
            PlayerController controller =
                player.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.speed = 3f;
            }
        }
    }
}