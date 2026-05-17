using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoes;
    public Vector2 spawnPosition = new Vector2(5, 18);

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (tetrominoes == null || tetrominoes.Length == 0)
        {
            Debug.LogError("Spawner: NO PREFABS ASSIGNED!");
            return;
        }

        int index = Random.Range(0, tetrominoes.Length);

        Instantiate(
            tetrominoes[index],
            new Vector3(spawnPosition.x, spawnPosition.y, 0),
            Quaternion.identity
        );
    }
}