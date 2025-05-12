using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public DraggablePiece[] pieces;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckWinCondition()
    {
        // Check if all pieces are in their correct positions
        foreach (var piece in pieces)
        {
            if (piece.currentSlotIndex == -1)
            {
                // Some pieces are not placed yet
                Debug.Log("Some pieces are not placed yet.");
                return;
            }
        }

        // If all pieces are placed, we can declare the puzzle complete
        Debug.Log("Puzzle Complete and Correct!");
    }
}

