using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoes;

    private bool spawning = false;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (spawning) return;

        spawning = true;

        int i = Random.Range(0, tetrominoes.Length);

        Instantiate(
            tetrominoes[i],
            new Vector3(5, 18, 0),
            Quaternion.identity
        );

        spawning = false;
    }
}