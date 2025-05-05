using UnityEngine;

// Assets/Scripts/Managers/DigGameManager.cs
using UnityEngine;
using TMPro;

public class DigGameManager : MonoBehaviour
{
    public static DigGameManager Instance;

    public int totalPieces = 5;
    public TextMeshProUGUI piecesCounterText;

    private int foundCount = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        // You can vary totalPieces by region if you like:
        // totalPieces = RegionManager.PiecesPerRegion;
        UpdateUI();
    }

    public void FoundPiece()
    {
        foundCount++;
        UpdateUI();

        if (foundCount >= totalPieces)
            Invoke(nameof(AllFound), 1f);
    }

    void UpdateUI()
    {
        piecesCounterText.text = $"{foundCount} / {totalPieces}";
    }

    void AllFound()
    {
        // e.g. load next scene or back to map
        MapManager.Instance.LoadSection("AssembleScene");
    }
}
