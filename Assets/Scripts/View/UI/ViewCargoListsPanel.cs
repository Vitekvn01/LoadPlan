using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCargoListsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _contentScrollView;
    [SerializeField] private GameObject _infoCargoListElementPrefab;
    private List<GameObject> InfoCargoListElements = new List<GameObject>();


    private List<List<Cargo>> _allCargoLists;

    public Action OnEventSetCargoLists;

    private void Start()
    {
        OnEventSetCargoLists += UpdateViewUI;
    }

    public void SetAllCargoLists(List<List<Cargo>> AllCargoLists)
    {
        _allCargoLists = AllCargoLists;
        OnEventSetCargoLists.Invoke();
    }

    private void UpdateViewUI()
    {
        DeleteInfoCargoListElements();

        for (int i = 0; i < _allCargoLists.Count; i++)
        {
            GameObject infoCargoListElement = Instantiate(_infoCargoListElementPrefab, _contentScrollView.transform);
            List<Cargo> cargos = _allCargoLists[i];
            infoCargoListElement.GetComponent<InfoCargoList>().SetCargoInfo(cargos[0], cargos.Count);
            InfoCargoListElements.Add(infoCargoListElement);
        }
    }

    private void DeleteInfoCargoListElements()
    {

        for (int i = 0; i < InfoCargoListElements.Count; i++)
        {
            Destroy(InfoCargoListElements[i]);
        }

        InfoCargoListElements.Clear();
    }
}
