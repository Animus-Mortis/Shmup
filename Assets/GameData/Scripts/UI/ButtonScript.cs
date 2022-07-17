using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent DownEvent;
    [SerializeField] private UnityEvent UpEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        DownEvent.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UpEvent.Invoke();
    }
}
