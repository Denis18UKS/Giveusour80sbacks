using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoes;

    public Vector3 spawnPosition = new Vector3(5, 16, 0);

    public void Spawn()
    {
        if (TetrisGameManager.instance.isGameOver)
            return;

        int random = Random.Range(0, tetrominoes.Length);

        Instantiate(
            tetrominoes[random],
            spawnPosition,
            Quaternion.identity
        );
    }
}