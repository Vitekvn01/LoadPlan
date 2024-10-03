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
        // ���� ���� �� �� ������ � ��������� ������
        if (!RectTransformUtility.RectangleContainsScreenPoint(
            GetComponent<RectTransform>(),
            eventData.position,
            null))
        {
            ClosePanel(); // ��������� ������
        }
    }

    // ����� �������� ������
    private void ClosePanel()
    {

        gameObject.SetActive(false);

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // �������� ����� ����� ������� ����
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            CheckClickOutside(eventData);  // ��������� ���� ��� ������
        }
    }

}
