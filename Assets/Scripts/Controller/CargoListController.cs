using UnityEngine;

public class CargoListController : MonoBehaviour
{
    [SerializeField] private GameObject UICreateCargoListPanelPrefab;
    [SerializeField] private GameObject SpawnerCargoPrefub;

    private CreateCargoListPanel _CreateCargoListPanel;

    private float _length = 0;
    private float _width = 0;
    private float _height = 0;
    private float _weight = 0;
    private float _count = 0;

    private bool _isTiering;
    private bool _isOnlyFloor;

    private void Awake()
    {
        InitCreateCargoListPanel();
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
        _CreateCargoListPanel = UIPanel.GetComponent<CreateCargoListPanel>();
    }

    #endregion

    #region Sub/Unsub event
    public void SubscriptCargoSettings()
    {
        _CreateCargoListPanel.OnCreateCargoButton += HoldCreateCargoButton;
        _CreateCargoListPanel.OnLengthChanged += HandleLengthChanged;
        _CreateCargoListPanel.OnWidthChanged += HandleWidthChanged;
        _CreateCargoListPanel.OnHeightChanged += HandleHeightChanged;
        _CreateCargoListPanel.OnWeightChanged += HandleWeightChanged;
        _CreateCargoListPanel.OnCountChanged += HandleCountChanged;
        _CreateCargoListPanel.OnTieringChanged += HandleTieringChanged;
        _CreateCargoListPanel.OnOnlyFloorChanged += HandleOnlyFloorChanged;
    }

    private void UnsubscriptCargoSettings()
    {
        _CreateCargoListPanel.OnCreateCargoButton -= HoldCreateCargoButton;
        _CreateCargoListPanel.OnLengthChanged -= HandleLengthChanged;
        _CreateCargoListPanel.OnWidthChanged -= HandleWidthChanged;
        _CreateCargoListPanel.OnHeightChanged -= HandleHeightChanged;
        _CreateCargoListPanel.OnWeightChanged -= HandleWeightChanged;
        _CreateCargoListPanel.OnCountChanged -= HandleCountChanged;
        _CreateCargoListPanel.OnTieringChanged -= HandleTieringChanged;
        _CreateCargoListPanel.OnOnlyFloorChanged -= HandleOnlyFloorChanged;
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

    private void HandleCountChanged(float newCount)
    {
        _count = newCount;
    }

    private void HandleTieringChanged(bool isTiering)
    {
        _isTiering = isTiering;
    }

    private void HandleOnlyFloorChanged(bool isOnlyFloor)
    {
        _isOnlyFloor = isOnlyFloor;
    }

    private void HoldCreateCargoButton()
    {
    }
    #endregion

} 

