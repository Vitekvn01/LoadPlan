using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoUploadController : SingletonBase<CargoUploadController>
{
    private int id;

    [SerializeField] private GameObject _cargoPrefub;

    private CargoManager _cargoManager;

    private bool _isManualUpload;

    private List<Cargo> _currentCargoList;

    private int _placedCount = 0;

    private void Start()
    {
        _cargoManager = gameObject.AddComponent<CargoManager>();
    }

    private void Update()
    {

    }

    public void HoldUploadButton()
    {
        // должен вызываться метод запуска алгоритма
        // передаём лист CargoListController.Instance.AllCargoLists, размеры грузовового отсека, и начальную позицию
    }

    public void StartManualUpload(List<Cargo> cargoList)
    {
        _placedCount = 1;
        _currentCargoList = cargoList;
        IMovable movable = CargoSpawn().GetComponent<IMovable>();
        Cursor3DController.Instance.GetIMovable(movable);
        Cursor3DController.Instance.OnPlacedEvent += NextCargoUpload;
    }

    private void NextCargoUpload()
    {
        if (_placedCount < _currentCargoList.Count)
        {
            _placedCount++;
            IMovable movable = CargoSpawn().GetComponent<IMovable>();
            Cursor3DController.Instance.GetIMovable(movable);
        }
        else
        {
            Cursor3DController.Instance.OnPlacedEvent -= NextCargoUpload;
        }
    }

    private GameObject CargoSpawn()
    {
        GameObject cargo = Instantiate(_cargoPrefub);
        BoxCollider collider = cargo.AddComponent<BoxCollider>();
        collider.size = cargo.transform.localScale;
        cargo.name = _currentCargoList[0].Name + ": " + _placedCount;
        collider.name = _currentCargoList[0].Name + ": " + _placedCount;
        Physics.SyncTransforms();
        return cargo;
    }
}
