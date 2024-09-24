using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CargoListController : SingletonBase<CargoListController>
{
    [Header("Префабы UI")]
    [SerializeField] private GameObject UICreateCargoListPanelPrefab;
    [SerializeField] private GameObject UIViewCargoListsPanelPrefab;

    private CreateCargoListPanel _createCargoListPanel;
    private ViewCargoListsPanel _viewCargoListsPanel;
    private CargoListLogic _cargoListLogic;

    private List<List<Cargo>> _allCargoLists = new List<List<Cargo>>();
    public List<List<Cargo>> AllCargoLists => _allCargoLists;

    private float _length = 0;
    private float _width = 0;
    private float _height = 0;
    private float _weight = 0;
    private int _count = 0;
    private string _name = "NoNameCargo";

    private bool _isTiering;
    private bool _isOnlyFloor;

    protected override void Awake()
    {
        InitCargoListLogic();
        InitCreateCargoListPanel();
        InitViewCargoLists();
    }

    private void Start()
    {
        SubscriptCargoSettings();
    }

    private void OnDisable()
    {
        UnsubscriptCargoSettings();
    }



    #region инициализация

    public void InitCreateCargoListPanel()
    {
        GameObject UIPanel = Instantiate(UICreateCargoListPanelPrefab);
        _createCargoListPanel = UIPanel.GetComponent<CreateCargoListPanel>();
    }

    public void InitCargoListLogic()
    {
        _cargoListLogic = new CargoListLogic(_allCargoLists);
    }

    public void InitViewCargoLists()
    {
        GameObject UIPanel = Instantiate(UIViewCargoListsPanelPrefab);
        _viewCargoListsPanel = UIPanel.GetComponent<ViewCargoListsPanel>();
    }

    #endregion

    #region Sub/Unsub event
    public void SubscriptCargoSettings()
    {
        _createCargoListPanel.OnCreateCargoButton += HoldCreateCargoListButton;
        _createCargoListPanel.OnLengthChanged += HandleLengthChanged;
        _createCargoListPanel.OnWidthChanged += HandleWidthChanged;
        _createCargoListPanel.OnHeightChanged += HandleHeightChanged;
        _createCargoListPanel.OnWeightChanged += HandleWeightChanged;
        _createCargoListPanel.OnCountChanged += HandleCountChanged;
        _createCargoListPanel.OnNameChanged += HandleNameChanged;
        _createCargoListPanel.OnTieringChanged += HandleTieringChanged;
        _createCargoListPanel.OnOnlyFloorChanged += HandleOnlyFloorChanged;
    }

    private void UnsubscriptCargoSettings()
    {
        _createCargoListPanel.OnCreateCargoButton -= HoldCreateCargoListButton;
        _createCargoListPanel.OnLengthChanged -= HandleLengthChanged;
        _createCargoListPanel.OnWidthChanged -= HandleWidthChanged;
        _createCargoListPanel.OnHeightChanged -= HandleHeightChanged;
        _createCargoListPanel.OnWeightChanged -= HandleWeightChanged;
        _createCargoListPanel.OnCountChanged -= HandleCountChanged;
        _createCargoListPanel.OnTieringChanged -= HandleTieringChanged;
        _createCargoListPanel.OnOnlyFloorChanged -= HandleOnlyFloorChanged;
    }

    #endregion

    #region обработка событий
    private void HandleLengthChanged(float newLength)
    {
        _length = newLength;
    }

    private void HandleWidthChanged(float newWidth)
    {
        _width = newWidth;
    }

    private void HandleHeightChanged(float newHeight)
    {
        _height = newHeight;
    }

    private void HandleWeightChanged(float newWeight)
    {
        _weight = newWeight;
    }

    private void HandleCountChanged(int newCount)
    {
        _count = newCount;
    }

    private void HandleNameChanged(string newName)
    {
        _name = newName;
    }

    private void HandleTieringChanged(bool isTiering)
    {
        _isTiering = isTiering;
    }

    private void HandleOnlyFloorChanged(bool isOnlyFloor)
    {
        _isOnlyFloor = isOnlyFloor;
    }

    private void HoldCreateCargoListButton()
    {
        if (_length != 0 && _height != 0 && _width != 0 && _weight != 0 && _count != 0)
        {
            _cargoListLogic.CreateCargoList(_length, _width, _height, _weight, _name, _isTiering, _isOnlyFloor, _count);
            _viewCargoListsPanel.SetAllCargoLists(_cargoListLogic.AllCargoLists); 
            Debug.Log("Всего листов: " + _cargoListLogic.AllCargoLists.Count);

        }
        else Debug.LogWarning("Заполните все поля!");


    }
    #endregion

} 

