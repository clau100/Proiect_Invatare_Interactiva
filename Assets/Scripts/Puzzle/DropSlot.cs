using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex; // Set this in the Inspector

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Dropped on Slot");

        // Get the dragged object
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            DraggablePiece piece = draggedObject.GetComponent<DraggablePiece>();

            // Snap the dragged object to this slot
            RectTransform draggedRectTransform = draggedObject.GetComponent<RectTransform>();
            draggedRectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            // Assign current slot index to the piece
            piece.currentSlotIndex = slotIndex;

            // Check if piece is in correct slot
            if (piece.correctSlotIndex == slotIndex)
            {
                Debug.Log("Correct placement!");
            }
            else
            {
                Debug.Log("Incorrect placement!");
                // Move the piece back to its original position if it's incorrectly placed
                draggedRectTransform.anchoredPosition = piece.originalPosition;
                piece.currentSlotIndex = -1; // Reset the current slot index
            }

            PuzzleManager.Instance.CheckWinCondition();
        }
    }
}