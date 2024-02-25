using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MicrowaveButtonController : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    private bool _isInside = false;
    private bool _isClicked = false;
    [SerializeField] private UnityEvent ClickAdditionalEvent;
    [SerializeField] private UnityEvent UnClickAdditionEvent;
    [SerializeField] private Sprite clickedSprite;
    private Image image;
    private Sprite unClickedSprite;

    private void Awake()
    {
        image = GetComponent<Image>();
        unClickedSprite = image.sprite;
    }

    private void Update()
    {
        if (!_isInside && _isClicked) TryToUnclick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TryToClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isInside = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TryToUnclick();
    }

    public void TryToClick()
    {
        if (!_isInside) return;
        if (_isClicked) return;

        if (clickedSprite != null) image.sprite = clickedSprite;

        _isClicked = true;
        ClickAdditionalEvent?.Invoke();
    }

    public void TryToUnclick()
    {
        if (!_isClicked) return;
        
        UnClickAdditionEvent?.Invoke();
        image.sprite = unClickedSprite;
        _isClicked = false;
    }

    public void DragDownObject(Transform objectToDrag)
    {
        objectToDrag.transform.position -= Vector3.up * 10f;
    }

    public void DragUpObject(Transform objectToDrag)
    {
        objectToDrag.transform.position += Vector3.up * 10f;
    }
}