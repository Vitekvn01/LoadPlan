using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateCargoListPanel : MonoBehaviour
{
    [Header("Поля ввода")]
    [SerializeField] private TMP_InputField _inputFieldLength;
    [SerializeField] private TMP_InputField _inputFieldHeight;
    [SerializeField] private TMP_InputField _inputFieldWidth;
    [SerializeField] private TMP_InputField _inputFieldWeight;
    [SerializeField] private TMP_InputField _inputFieldCount;

    [SerializeField] private Toggle _toogleTiering;
    [SerializeField] private Toggle _toogleFloor;



    private float _length = 0;
    private float _height = 0;
    private float _width = 0;
    private float _weight = 0;
    private float _count = 0;

    private bool _isTiering;
    private bool _isOnlyFloor;
    #region API
    public event Action OnCreateCargoButton;

    public event Action<float> OnLengthChanged;
    public event Action<float> OnWidthChanged;
    public event Action<float> OnHeightChanged;
    public event Action<float> OnWeightChanged;
    public event Action<float> OnCountChanged;

    public event Action<bool> OnTieringChanged;
    public event Action<bool> OnOnlyFloorChanged;

    #endregion
    private void Awake()
    {
        // подписка на обновление inputField
        _inputFieldLength.onValueChanged.AddListener(OnInputFieldLengthChanged);
        _inputFieldWidth.onValueChanged.AddListener(OnInputFieldWidthChanged);
        _inputFieldHeight.onValueChanged.AddListener(OnInputFieldHeightChanged);
        _inputFieldWeight.onValueChanged.AddListener(OnInputFieldWeightChanged);
        _inputFieldCount.onValueChanged.AddListener(OnInputFieldCountChanged);
        _toogleTiering.onValueChanged.AddListener(OnToogleTieringChanged);
        _toogleFloor.onValueChanged.AddListener(OnToogleOnlyFloorChanged);
    }

    private void OnInputFieldCountChanged(string newValue)
    {
        _count = float.Parse(newValue);
        OnCountChanged?.Invoke(_count);
    }

    private void OnInputFieldLengthChanged(string newValue)
    {
        _length = float.Parse(newValue);
        OnLengthChanged?.Invoke(_length);
    }

    private void OnInputFieldWidthChanged(string newValue)
    {
        _width = float.Parse(newValue);
        OnWidthChanged?.Invoke(_width);
    }

    private void OnInputFieldHeightChanged(string newValue)
    {
        _height = float.Parse(newValue);
        OnHeightChanged?.Invoke(_height);
    }

    private void OnInputFieldWeightChanged(string newValue)
    {
        _weight = float.Parse(newValue);
        OnWeightChanged?.Invoke(_weight);
    }

    private void OnToogleTieringChanged(bool change)
    {
        _isTiering = change;
        OnTieringChanged?.Invoke(_isTiering);
    }

    private void OnToogleOnlyFloorChanged(bool change)
    {
        _isOnlyFloor = change;
        OnOnlyFloorChanged?.Invoke(_isOnlyFloor);
    }
    public void CreateCargoButton()
    {
        OnCreateCargoButton?.Invoke();
    }
}
