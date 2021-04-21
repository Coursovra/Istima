using UnityEngine;
using UnityEngine.EventSystems;

public class CloseInfoPanelView : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
