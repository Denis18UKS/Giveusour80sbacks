using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoes;

    // точка спавна
    public Vector3 spawnPosition = new Vector3(5, 18, 0);

    public void Spawn()
    {
        if (TetrisGameManager.instance == null)
            return;

        if (TetrisGameManager.instance.isGameOver)
            return;

        if (tetrominoes == null || tetrominoes.Length == 0)
        {
            Debug.LogError("NO TETROMINOS!");
            return;
        }

        int random = Random.Range(0, tetrominoes.Length);

        GameObject prefab = tetrominoes[random];

        if (prefab == null)
        {
            Debug.LogError("NULL PREFAB IN ARRAY!");
            return;
        }

        Instantiate(
            prefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}