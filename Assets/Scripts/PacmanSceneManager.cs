using System.Collections;
using UnityEngine;

public class PacmanSceneManager : MonoBehaviour
{
    public Transform spawnPoint;

    IEnumerator Start()
    {
        yield return null;

        GameState.isPacmanMode = true;

        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = spawnPoint.position;

            PlayerController controller =
                player.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.ApplyMode();
                Debug.Log("PACMAN SPEED = " + controller.speed);
            }
        }
    }
}