using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdditionalCargoListPanel : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {


    }

    public void CheckClickOutside(PointerEventData eventData)
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

    // Метод закрытия панели
    private void ClosePanel()
    {

        gameObject.SetActive(false);

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверка клика левой кнопкой мыши
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            CheckClickOutside(eventData);  // Проверяем клик вне панели
        }
    }

}
