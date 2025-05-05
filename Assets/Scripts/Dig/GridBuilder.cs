using UnityEngine;

// Assets/Scripts/Dig/GridBuilder.cs
using System.Linq;

public class GridBuilder : MonoBehaviour
{
    public GameObject digCellPrefab;
    public int rows = 8, cols = 8;
    public float spacing = 1.0f;

    void Start()
    {
        var cells = new DigCell[rows * cols];
        int index = 0;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                var go = Instantiate(digCellPrefab, transform);
                go.transform.localPosition = new Vector3(x * spacing, y * spacing, 0);
                go.name = $"Cell_{x}_{y}";
                cells[index++] = go.GetComponent<DigCell>();
            }
        }

        // Randomize which cells have pieces
        var shuffled = cells.OrderBy(_ => Random.value).ToArray();
        int toPlace = RegionManager.PiecesPerRegion;
        for (int i = 0; i < toPlace; i++)
            shuffled[i].HasPiece = true;
    }
}

