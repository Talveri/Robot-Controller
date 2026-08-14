using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeBlockDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform list;

    private Canvas rootCanvas;
    private Color originalColor;
    private bool isOutside;
    private Image blockImage;
    private Transform originalParent;
    private int originalIndex;
    private GameObject placeholder;

    void Start()
    {
        blockImage = GetComponent<Image>();
        rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalIndex  = transform.GetSiblingIndex();
        originalColor  = blockImage.color;

        placeholder = new GameObject("Placeholder");
        placeholder.transform.SetParent(originalParent);

        LayoutElement ownLE = GetComponent<LayoutElement>();
        LayoutElement le    = placeholder.AddComponent<LayoutElement>();
        le.preferredHeight  = ownLE.preferredHeight;
        le.preferredWidth   = ownLE.preferredWidth;

        Image img  = placeholder.AddComponent<Image>();
        img.color  = new Color(1f, 0.9f, 0.1f, 0.5f);

        RectTransform rt = placeholder.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(ownLE.preferredWidth, ownLE.preferredHeight);

        placeholder.transform.SetSiblingIndex(originalIndex);
        transform.SetParent(rootCanvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        isOutside = !RectTransformUtility.RectangleContainsScreenPoint(
            list, eventData.position, eventData.pressEventCamera);

        blockImage.color = isOutside ? new Color(1f, 0.4f, 0.4f, 1f) : originalColor;

        int newIndex = Enumerable.Range(0, originalParent.childCount)
            .FirstOrDefault(i => transform.position.y > originalParent.GetChild(i).position.y);

        placeholder.transform.SetSiblingIndex(newIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isOutside)
        {
            Destroy(gameObject);
            Destroy(placeholder);
            
            CodeListEvents.OnDeleteBlock?.Invoke();

            return;
        }

        transform.SetParent(originalParent);
        transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        CodeListEvents.OnSwitchBlock?.Invoke();
        Destroy(placeholder);
    }
}
