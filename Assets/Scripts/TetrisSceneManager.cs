using UnityEngine;

public class TetrisSceneManager : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("TetrisScene: Player is not needed here (ignored).");
            return;
        }

        Debug.Log("Player exists (Tetris mode)");
    }
}