using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TetrisGameManager : MonoBehaviour
{
    public static TetrisGameManager instance;

    [Header("Game")]
    public bool isGameOver;
    public int score;
    public int winScore = 10;
    public int linesCleared;

    [Header("Spawner")]
    public Spawner spawner;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeSpeed = 2f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip winSound;

    private bool winStarted;

    void Awake()
    {
        instance = this;
        isGameOver = false;
        score = 0;
        linesCleared = 0;
        GridManager.ResetGrid();
    }

    void Start()
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
        }

        SpawnNextPiece();
    }

    void Update()
    {
        if (!isGameOver || Keyboard.current == null)
            return;

        if (Keyboard.current.rKey.wasPressedThisFrame)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            SceneManager.LoadScene("GameScene");
    }

    public void SpawnNextPiece()
    {
        if (isGameOver)
            return;

        if (spawner == null)
        {
            Debug.LogError("Tetris: spawner is not assigned.");
            GameOver();
            return;
        }

        spawner.Spawn();
    }

    public void PieceLocked(int deletedLines)
    {
        if (isGameOver)
            return;

        linesCleared += deletedLines;
        score++;

        Debug.Log("SCORE: " + score + ", LINES: " + linesCleared);

        if (score >= winScore)
        {
            if (!winStarted)
                StartCoroutine(WinCoroutine());

            return;
        }

        SpawnNextPiece();
    }

    IEnumerator WinCoroutine()
    {
        winStarted = true;
        isGameOver = true;

        Debug.Log("YOU WIN");

        if (audioSource != null && winSound != null)
            audioSource.PlayOneShot(winSound);

        if (fadeImage != null)
        {
            float alpha = fadeImage.color.a;

            while (alpha < 1f)
            {
                alpha = Mathf.Min(1f, alpha + Time.deltaTime * fadeSpeed);
                Color color = fadeImage.color;
                color.a = alpha;
                fadeImage.color = color;
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
        Debug.Log("GAME OVER. Press R to restart or Esc to return.");
    }
}
