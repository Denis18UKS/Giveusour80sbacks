using UnityEngine;

public class TetrominoGenerator : MonoBehaviour
{
    public GameObject blockPrefab;

    private readonly Vector2[][] shapes = new Vector2[][]
    {
        // I
        new Vector2[] { new(0,0), new(1,0), new(2,0), new(3,0) },

        // O
        new Vector2[] { new(0,0), new(1,0), new(0,1), new(1,1) },

        // T
        new Vector2[] { new(0,0), new(-1,0), new(1,0), new(0,1) },

        // L
        new Vector2[] { new(0,0), new(0,1), new(0,-1), new(1,-1) },

        // J
        new Vector2[] { new(0,0), new(0,1), new(0,-1), new(-1,-1) },

        // S
        new Vector2[] { new(0,0), new(-1,0), new(0,1), new(1,1) },

        // Z
        new Vector2[] { new(0,0), new(1,0), new(0,1), new(-1,1) }
    };

    public GameObject Spawn()
    {
        GameObject piece = new GameObject("Tetromino");

        int index = Random.Range(0, shapes.Length);

        foreach (Vector2 pos in shapes[index])
        {
            GameObject block = Instantiate(blockPrefab, piece.transform);
            block.transform.localPosition = new Vector3(pos.x, pos.y, 0);
        }

        piece.AddComponent<Tetromino>();

        return piece;
    }
}