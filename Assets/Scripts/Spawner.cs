using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Kept so the existing scene remains compatible. The old visual prefabs are no longer
    // used because they are not made from four independent grid cells.
    [HideInInspector] public GameObject[] tetrominoes;

    public Vector2Int spawnCell = new Vector2Int(4, 18);

    private readonly List<int> bag = new List<int>();

    private static readonly Vector2Int[][] shapes =
    {
        new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0) }, // I
        new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) },   // O
        new[] { new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1) },  // T
        new[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) }, // J
        new[] { new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },   // L
        new[] { new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0) },   // S
        new[] { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) }    // Z
    };

    private static readonly Color[] colors =
    {
        new Color(0.10f, 0.90f, 0.95f),
        new Color(1.00f, 0.85f, 0.10f),
        new Color(0.75f, 0.25f, 0.95f),
        new Color(0.20f, 0.40f, 1.00f),
        new Color(1.00f, 0.50f, 0.10f),
        new Color(0.25f, 0.90f, 0.30f),
        new Color(0.95f, 0.20f, 0.25f)
    };

    public void Spawn()
    {
        if (TetrisGameManager.instance == null || TetrisGameManager.instance.isGameOver)
            return;

        int shapeIndex = TakeNextShapeIndex();
        GameObject pieceObject = new GameObject("Tetromino");
        Tetromino piece = pieceObject.AddComponent<Tetromino>();

        piece.Initialize(shapes[shapeIndex], colors[shapeIndex], shapeIndex != 1, spawnCell);

        if (piece.IsInValidPosition())
            return;

        Destroy(pieceObject);
        TetrisGameManager.instance.GameOver();
    }

    int TakeNextShapeIndex()
    {
        if (bag.Count == 0)
        {
            for (int i = 0; i < shapes.Length; i++)
                bag.Add(i);

            for (int i = bag.Count - 1; i > 0; i--)
            {
                int swapIndex = Random.Range(0, i + 1);
                int value = bag[i];
                bag[i] = bag[swapIndex];
                bag[swapIndex] = value;
            }
        }

        int nextShape = bag[0];
        bag.RemoveAt(0);
        return nextShape;
    }
}
