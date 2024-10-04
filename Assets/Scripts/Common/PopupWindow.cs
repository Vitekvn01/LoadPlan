using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupWindow : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            CheckClickOutside(eventData);
        }
    }

    private void CheckClickOutside(PointerEventData eventData)
    {
        // Если клик не по панели — закрываем панель
        if (!RectTransformUtility.RectangleContainsScreenPoint(
            GetComponent<RectTransform>(),
            eventData.position,
            null))
        {
            ClosePanel(); // Закрываем панель
        }
    }

    protected virtual void ClosePanel()
    {
        gameObject.SetActive(false);
    }



}
