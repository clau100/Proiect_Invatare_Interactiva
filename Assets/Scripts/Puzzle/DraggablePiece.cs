using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePiece : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;

    public int correctSlotIndex; // Set this in the Inspector
    public int currentSlotIndex = -1;

    [HideInInspector] public Vector2 originalPosition;

    public bool isUnlocked = false;

    public void UnlockPiece()
    {
        isUnlocked = true;
        gameObject.SetActive(true); // If pieces start hidden
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Return to original position if not dropped on a valid slot
        if (!eventData.pointerEnter || eventData.pointerEnter.GetComponent<DropSlot>() == null)
        {
            rectTransform.anchoredPosition = originalPosition;
            currentSlotIndex = -1;
        }
    }
}
