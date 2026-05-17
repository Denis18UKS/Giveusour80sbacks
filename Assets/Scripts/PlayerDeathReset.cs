using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathReset : MonoBehaviour
{
    private bool isReloading = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isReloading) return;

        if (other.CompareTag("Ghost"))
        {
            isReloading = true;
            Invoke(nameof(ReloadScene), 0.5f);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}