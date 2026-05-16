using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractionPacmanCartridge : MonoBehaviour
{
    public Image fadeImage;
    public AudioSource audioSource;
    public AudioClip cartridgeSound;

    private bool pickedUp = false;

    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!pickedUp && other.CompareTag("Player"))
        {
            pickedUp = true;

            // Выключаем collider
            if (col != null)
            {
                col.enabled = false;
            }

            // Скрываем объект
            foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.enabled = false;
            }

            StartCoroutine(TransitionToPacman());
        }
    }

    IEnumerator TransitionToPacman()
    {
        // PLAY SOUND
        if (audioSource != null && cartridgeSound != null)
        {
            audioSource.PlayOneShot(cartridgeSound);
        }

        // FADE TO BLACK
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;

            if (fadeImage != null)
            {
                Color color = fadeImage.color;
                color.a = Mathf.Lerp(0, 1, time);
                fadeImage.color = color;
            }

            yield return null;
        }

        // WAIT SOUND
        yield return new WaitForSeconds(1.5f);

        // LOAD SCENE
        SceneManager.LoadScene("PacmanScene");
    }
}