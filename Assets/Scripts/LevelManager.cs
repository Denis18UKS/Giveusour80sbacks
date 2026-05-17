using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Image fadeImage;
    public AudioSource audioSource;

    private int totalDots;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        totalDots = FindObjectsOfType<Dot>().Length;
    }

    public void DotCollected()
    {
        totalDots--;

        if (totalDots <= 0)
        {
            StartCoroutine(FadeAndLoad());
        }
    }

    IEnumerator FadeAndLoad()
    {
        GameState.isPacmanMode = true;

        if (audioSource != null)
            audioSource.Play();

        Color color = fadeImage.color;

        while (color.a < 1)
        {
            color.a += Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene("GameScene");
    }
}