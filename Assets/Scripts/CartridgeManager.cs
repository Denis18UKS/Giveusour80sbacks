using UnityEngine;

public class CartridgeManager : MonoBehaviour
{
    [Header("Objects In Scene")]
    public GameObject pacmanObject;
    public GameObject tetrisObject;
    public GameObject marioObject;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private int currentIndex = 0;

    private Cartridge.CartridgeType[] order =
    {
        Cartridge.CartridgeType.Pacman,
        Cartridge.CartridgeType.Tetris,
        Cartridge.CartridgeType.Mario
    };

    void Start()
    {
        SpawnNext();
    }

    public void OnCartridgeCollected(Cartridge.CartridgeType type)
    {
        if (type == order[currentIndex])
        {
            currentIndex++;
            SpawnNext();
        }
    }

    void SpawnNext()
    {
        if (currentIndex >= order.Length)
        {
            Debug.Log("Все картриджи собраны!");
            return;
        }

        GameObject obj = GetObject(order[currentIndex]);

        Transform spawnPoint =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        obj.transform.position = spawnPoint.position;
        obj.SetActive(true);

        Cartridge c = obj.GetComponent<Cartridge>();
        c.Init(this);
    }

    GameObject GetObject(Cartridge.CartridgeType type)
    {
        switch (type)
        {
            case Cartridge.CartridgeType.Pacman:
                return pacmanObject;

            case Cartridge.CartridgeType.Tetris:
                return tetrisObject;

            case Cartridge.CartridgeType.Mario:
                return marioObject;
        }

        return null;
    }
}