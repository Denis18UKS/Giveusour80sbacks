using UnityEngine;

public class CartridgeManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject pacmanPrefab;
    public GameObject tetrisPrefab;
    public GameObject marioPrefab;

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

        GameObject prefabToSpawn = GetPrefab(order[currentIndex]);
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject obj = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        Cartridge c = obj.GetComponent<Cartridge>();
        c.Init(this);
    }

    GameObject GetPrefab(Cartridge.CartridgeType type)
    {
        switch (type)
        {
            case Cartridge.CartridgeType.Pacman:
                return pacmanPrefab;

            case Cartridge.CartridgeType.Tetris:
                return tetrisPrefab;

            case Cartridge.CartridgeType.Mario:
                return marioPrefab;
        }

        return null;
    }
}