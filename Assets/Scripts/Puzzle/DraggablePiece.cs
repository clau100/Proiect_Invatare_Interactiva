using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggablePiece : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;

    public int correctSlotIndex; // Set this in the Inspector
    public int currentSlotIndex = -1;

    [HideInInspector] public Vector2 originalPosition;

    public bool isUnlocked = false;
    private Image pieceImage;

    public void UnlockPiece()
    {
        isUnlocked = true;
        canvasGroup.blocksRaycasts = true; // Enable drop interactions
        SetAlpha(1f); // Fully visible
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        pieceImage = GetComponent<Image>();
    }

    private void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
        if (!isUnlocked)
        {
            canvasGroup.blocksRaycasts = false; // Prevents triggering drop zones
            SetAlpha(0.6f);
        }
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isUnlocked) return; // Prevent dragging if locked
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isUnlocked) return; // Prevent dragging if locked
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isUnlocked) return; // Prevent dragging if locked
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Return to original position if not dropped on a valid slot
        if (!eventData.pointerEnter || eventData.pointerEnter.GetComponent<DropSlot>() == null)
        {
            rectTransform.anchoredPosition = originalPosition;
            currentSlotIndex = -1;
        }
    }

    private void SetAlpha(float alpha)
    {
        if (pieceImage != null)
        {
            Color c = pieceImage.color;
            c.a = alpha;
            pieceImage.color = c;
        }
    }
}
