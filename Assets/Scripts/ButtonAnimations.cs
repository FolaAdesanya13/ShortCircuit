using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private Vector3 originalScale;
    private float hoverScaleMult = 1.2f;
    void Awake()
    {
        originalScale = transform.localScale == Vector3.zero ? Vector3.one : transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScaleMult;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = originalScale * hoverScaleMult;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
