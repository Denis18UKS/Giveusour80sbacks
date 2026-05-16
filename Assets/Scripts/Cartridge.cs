using UnityEngine;

public class Cartridge : MonoBehaviour
{
    public CartridgeType type;

    public enum CartridgeType
    {
        Pacman,
        Tetris,
        Mario
    }

    private CartridgeManager manager;
    private bool picked = false;

    public void Init(CartridgeManager m)
    {
        manager = m;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (picked) return;

        if (other.CompareTag("Player"))
        {
            picked = true;
            manager.OnCartridgeCollected(type);
            Destroy(gameObject);
        }
    }
}