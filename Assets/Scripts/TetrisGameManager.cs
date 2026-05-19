using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TetrisGameManager : MonoBehaviour
{
    public static TetrisGameManager instance;

    [Header("Game")]
    public bool isGameOver = false;

    public int score = 0;
    public int winScore = 10;

    [Header("Spawner")]
    public Spawner spawner;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeSpeed = 2f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip winSound;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnNextPiece();

        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0;
            fadeImage.color = c;
        }
    }

    public void SpawnNextPiece()
    {
        if (isGameOver)
            return;

        spawner.Spawn();
    }

    public void PieceLocked()
    {
        AddScore(1);

        SpawnNextPiece();
    }

    public void AddScore(int amount)
    {
        score += amount;

        Debug.Log("SCORE: " + score);

        if (score >= winScore)
        {
            StartCoroutine(WinCoroutine());
        }
    }

    IEnumerator WinCoroutine()
    {
        isGameOver = true;

        Debug.Log("YOU WIN");

        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }

        if (fadeImage != null)
        {
            float a = 0;

            while (a < 1)
            {
                a += Time.deltaTime * fadeSpeed;

                Color c = fadeImage.color;
                c.a = a;

                fadeImage.color = c;

                yield return null;
            }
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("GameScene");
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        Debug.Log("GAME OVER");
    }
}