// Assets/Scripts/Dig/DigCell.cs
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class DigCell : MonoBehaviour
{
    public bool HasPiece = false;
    private bool dug = false;

    void OnMouseDown()
    {
        if (dug) return;
        dug = true;

        // Remove the tile
        Destroy(gameObject);

        // If it had a piece, inform the manager
        if (HasPiece)
            DigGameManager.Instance.FoundPiece();
    }
}

