using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoCargoListElement : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI _lengthText;
    public TextMeshProUGUI _widthText;
    public TextMeshProUGUI _heightText;
    public TextMeshProUGUI _weightText;
    public TextMeshProUGUI _countText;
    public TextMeshProUGUI _nameText;

    public Toggle _toogleTiering;
    public Toggle _toogleFloor;

    private List<Cargo> _cargoList;

    public GameObject AdditionalPanel;



    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            AdditionalPanel.SetActive(true);
            // Устанавливаем родителя - корневой Canvas
            RectTransform canvasRectTransform = gameObject.GetComponentInParent<Canvas>().transform as RectTransform;
            AdditionalPanel.transform.SetParent(canvasRectTransform, false);

            Vector2 localPoint;

            // Преобразуем экранные координаты в локальные координаты Canvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out localPoint);

            RectTransform newElementRectTransform = AdditionalPanel.GetComponent<RectTransform>();

            // Вычисляем половину ширины и высоты
            Vector2 halfSize = new Vector2(newElementRectTransform.rect.width / 2, - newElementRectTransform.rect.height / 2);


            // Устанавливаем позицию нового элемента на курсор мыши
            AdditionalPanel.GetComponent<RectTransform>().anchoredPosition = localPoint + halfSize;
        }
    }

    public void SetCargoInfo(List<Cargo> cargos, Cargo cargo, int count)
    {
        _cargoList = cargos;
        _lengthText.text = "Length:" + cargo.Length.ToString();
        _widthText.text = "Width:" + cargo.Width.ToString();
        _heightText.text = "Height:" + cargo.Height.ToString();
        _weightText.text = "Weight:" + cargo.Weight.ToString();
        _nameText.text = cargo.Name.ToString();
        _countText.text = "Count:" + count.ToString();
        _toogleTiering.isOn = cargo.IsTiering;
        _toogleFloor.isOn = cargo.IsOnlyFloor;
    }

}
