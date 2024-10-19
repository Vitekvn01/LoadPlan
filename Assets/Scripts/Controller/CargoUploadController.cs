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
        IMovable movable = CreateCargo3DView().GetComponent<IMovable>();
        Cursor3DController.Instance.GetIMovable(movable);
        Cursor3DController.Instance.OnPlacedEvent += NextCargoUpload;
    }

    private void NextCargoUpload()
    {
        if (_placedCount < _currentCargoList.Count)
        {
            _placedCount++;
            IMovable movable = CreateCargo3DView().GetComponent<IMovable>();
            Cursor3DController.Instance.GetIMovable(movable);
        }
        else
        {
            Cursor3DController.Instance.OnPlacedEvent -= NextCargoUpload;
        }
    }

    private GameObject CreateCargo3DView()
    {
        GameObject cargo = Instantiate(_cargoPrefub, new Vector3(0, 0, 0), Quaternion.identity);
        Cargo3DView cargo3DView = cargo.GetComponent<Cargo3DView>();
        SetParameters(cargo3DView, _currentCargoList[_placedCount - 1]);
        SetScaleObject(cargo, cargo3DView);
        cargo.name = _currentCargoList[_placedCount - 1].Name + "_" + (_placedCount - 1).ToString();
        Physics.SyncTransforms();
        return cargo;
    }

    private static void SetScaleObject(GameObject cargo, Cargo3DView cargo3DView)
    {
        cargo.transform.localScale = new Vector3(cargo3DView.Length, cargo3DView.Height, cargo3DView.Width);
    }

    private void SetParameters(Cargo3DView cargo3DView, Cargo cargo)
    {
        cargo3DView.SetParameters(cargo.Length, cargo.Height, cargo.Width, cargo.Weight, cargo.Name, cargo.IsTiering, cargo.IsOnlyFloor);
        cargo.SetCargo3DView(cargo3DView);
    }



}
