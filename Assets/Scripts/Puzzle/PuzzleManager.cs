using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

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
        if (GameManager.Instance.placedPieces.Count == pieces.Length)
        {
            // Puzzle is complete
            Debug.Log("Puzzle Complete and Correct!");
            GameManager.Instance.puzzleCompleted = true;  // Set puzzle completion flag to true

            // Call the method to return to quiz
            PuzzleManager.Instance.Invoke("ReturnToQuiz", 2f);
        }
        else
        {
            Debug.Log("Puzzle not complete yet.");
        }
    }

    void Start()
    {
        foreach (var piece in pieces)
        {
            // Restore previously placed pieces
            if (GameManager.Instance.placedPieces.Contains(piece.correctSlotIndex))
            {
                DropSlot slot = FindObjectsByType<DropSlot>(FindObjectsSortMode.None).First(s => s.slotIndex == piece.correctSlotIndex);
                piece.rectTransform.anchoredPosition = slot.GetComponent<RectTransform>().anchoredPosition;
                piece.currentSlotIndex = piece.correctSlotIndex;
                piece.UnlockPiece();
            }
        }

        UnlockRandomPiece();
    }

    public void UnlockRandomPiece()
    {
        List<DraggablePiece> lockedPieces = pieces.Where(p => !p.isUnlocked && !GameManager.Instance.placedPieces.Contains(p.correctSlotIndex)).ToList();

        if (lockedPieces.Count > 0)
        {
            int index = Random.Range(0, lockedPieces.Count);
            lockedPieces[index].UnlockPiece();
        }
    }

    void ReturnToQuiz()
    {
        if (GameManager.Instance.returnToQuiz)
        {
            GameManager.Instance.returnToQuiz = false;
            SceneManager.LoadScene("Quiz"); 
        }
    }
}

