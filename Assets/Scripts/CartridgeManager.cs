using UnityEngine;

public class CartridgeManager : MonoBehaviour
{
    public GameObject pacmanObject;
    public GameObject tetrisObject;
    public GameObject marioObject;

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
        HideAll();
        SpawnNext();
    }

    void HideAll()
    {
        pacmanObject.SetActive(false);
        tetrisObject.SetActive(false);
        marioObject.SetActive(false);
    }

    public void OnCartridgeCollected(Cartridge.CartridgeType type)
    {
        if (type == order[currentIndex])
        {
            GetObject(type).SetActive(false);

            currentIndex++;
            SpawnNext();
        }
    }

    void SpawnNext()
    {
        if (currentIndex >= order.Length)
        {
            Debug.Log("ВСЕ КАРТРИДЖИ СОБРАНЫ");
            return;
        }

        HideAll();

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