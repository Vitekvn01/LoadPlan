using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCargoListsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _contentScrollView;
    [SerializeField] private GameObject _infoCargoListElementPrefab;
    private List<GameObject> _infoCargoListElements = new List<GameObject>();

    private List<List<Cargo>> _allCargoLists;

    private void Start()
    {
        CargoListController.Instance.OnCargoListsChanged += UpdateViewUI;
    }

    public void SetAllCargoLists(List<List<Cargo>> AllCargoLists)
    {
        _allCargoLists = AllCargoLists;
    }

    private void UpdateViewUI()
    {
        DeleteInfoCargoListElements();

        for (int i = 0; i < _allCargoLists.Count; i++)
        {
            GameObject infoCargoListElement = Instantiate(_infoCargoListElementPrefab, _contentScrollView.transform);
            List<Cargo> cargos = _allCargoLists[i];
            infoCargoListElement.GetComponent<InfoCargoListElement>().SetCargoInfo(_allCargoLists[i], cargos[0], cargos.Count);
            _infoCargoListElements.Add(infoCargoListElement);
        }
    }

    private void DeleteInfoCargoListElements()
    {

        for (int i = 0; i < _infoCargoListElements.Count; i++)
        {
            Destroy(_infoCargoListElements[i]);
        }

        _infoCargoListElements.Clear();
    }
}
