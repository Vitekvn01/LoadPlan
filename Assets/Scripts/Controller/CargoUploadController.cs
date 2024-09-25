using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoUploadController : MonoBehaviour
{
    private Autoload _cargoManager;

    private void Start()
    {
        _cargoManager = gameObject.AddComponent<Autoload>();

    }

    public void HoldUploadButton()
    {
        // должен вызываться метод запуска алгоритма
        // передаём лист CargoListController.Instance.AllCargoLists, размеры грузовового отсека, и начальную позицию
    }
}
