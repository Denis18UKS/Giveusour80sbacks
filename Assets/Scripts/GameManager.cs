using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;

    public static void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }
}