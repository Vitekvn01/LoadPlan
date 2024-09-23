using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoListLogic
{
    private List<List<Cargo>> _allCargoLists = new List<List<Cargo>>();

    public List<List<Cargo>> AllCargoLists => _allCargoLists;

    public void CreateCargoList(float length, float width, float height, float weight, string name, bool isTiering, bool isOnlyFloor,  int count)
    {
        List<Cargo> CargoList = new List<Cargo>();

        for (int i = 0; i < count; i++)
        {
            Cargo cargo = new Cargo(length, width, height, weight, name, isTiering, isOnlyFloor);
            CargoList.Add(cargo);
        }

        _allCargoLists.Add(CargoList);
    }

}
