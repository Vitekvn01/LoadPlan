using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoListLogic
{
    private List<List<Cargo>> _allCargoLists = new List<List<Cargo>>();

    public List<List<Cargo>> AllCargoLists => _allCargoLists;

    public void CreateCargoList(float length, float width, float height, float weight, string name, bool isTiering, bool isOnlyFloor, int count)
    {
        if (length <= 0 || width <= 0 || height <= 0 || weight <= 0 || count <= 0)
        {
            Debug.LogError("Параметры груза должны быть положительными числами.");
            return;
        }

        List<Cargo> cargoList = new List<Cargo>();

        for (int i = 0; i < count; i++)
        {
            Cargo cargo = new Cargo(length, width, height, weight, name, isTiering, isOnlyFloor);
            cargoList.Add(cargo);
        }

        _allCargoLists.Add(cargoList);
    }
}
