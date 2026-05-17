using UnityEngine;

public class TetrisGameManager : MonoBehaviour
{
    public static TetrisGameManager instance;

    public float fallSpeed = 1f;
    public bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER");
    }
}