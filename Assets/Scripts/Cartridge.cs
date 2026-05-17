using UnityEngine;

public class Cartridge : MonoBehaviour
{
    public enum CartridgeType
    {
        Pacman,
        Tetris,
        Mario
    }

    public CartridgeType type;

    private CartridgeManager manager;

    public void Init(CartridgeManager m)
    {
        manager = m;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            manager.OnCartridgeCollected(type);

            gameObject.SetActive(false);
        }
    }
}