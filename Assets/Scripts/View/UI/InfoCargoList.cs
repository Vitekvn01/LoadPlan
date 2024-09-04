using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoCargoList : MonoBehaviour
{
    public TextMeshProUGUI _lengthText;
    public TextMeshProUGUI _widthText;
    public TextMeshProUGUI _heightText;
    public TextMeshProUGUI _weightText;
    public TextMeshProUGUI _countText;

    public Toggle _toogleTiering;
    public Toggle _toogleFloor;

    public void SetCargoInfo(Cargo cargo, int count)
    {
        _lengthText.text = "Length:" + cargo.Length.ToString();
        _widthText.text = "Width:" + cargo.Width.ToString();
        _heightText.text = "Height:" + cargo.Height.ToString();
        _weightText.text = "Weight:" + cargo.Weight.ToString();
        _countText.text = "Count:" + count.ToString();
        _toogleTiering.isOn = cargo.IsTiering;
        _toogleFloor.isOn = cargo.IsOnlyFloor;
    }

}
