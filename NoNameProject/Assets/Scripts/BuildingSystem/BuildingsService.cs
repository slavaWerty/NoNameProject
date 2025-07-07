using BuildingSystem;
using Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsService : IInitzializable, IDisposable
{
    public Action InteractSignalAdded;
    private event Action FactoryTypeIdChanged;

    private BuildingsSpawner _grid;
    private List<Button> _buttons;
    private List<InteractSignal> _ineractSignals;

    private string _currectFactoryTypeID;

    public List<InteractSignal> InteractSignals => _ineractSignals;

    public BuildingsService(BuildingsSpawner grid,
        List<Button> buttons)
    {
        _ineractSignals = new List<InteractSignal>();
        _grid = grid;
        _buttons = buttons;
    }

    private void PlaseBuilding(string typePlaceBuilding, int level)
    {
        _grid.StartPlacingBuilding(typePlaceBuilding, level);
        Debug.Log($"BuildingService{level}");
    }

    public void Update()
    {
        _grid.Update();
    }

    public void Initzialize()
    {
        _grid.Initzialize();

        FactoryTypeIdChanged += ButtonInitzialize;

        _grid.GridChanged += OnGridChanged;
    }

    private void ButtonInitzialize()
    {
        _buttons[0].onClick.AddListener(() =>
        {
            PlaseBuilding(_currectFactoryTypeID, 1);
        });

        _buttons[1].onClick.AddListener(() =>
        {
            PlaseBuilding(_currectFactoryTypeID, 2);
        });
    }

    public void Dispose()
    {
        FactoryTypeIdChanged -= ButtonInitzialize;
        ButtonDispose();

        _grid.GridChanged -= OnGridChanged;
    }

    private void ButtonDispose()
    {
        _buttons[0].onClick.RemoveListener(() =>
        {
            PlaseBuilding(_currectFactoryTypeID, 1);
        });

        _buttons[1].onClick.RemoveListener(() =>
        {
            PlaseBuilding(_currectFactoryTypeID, 2);
        });
    }

    private void OnGridChanged()
    {
        foreach (var building in _grid.Buildings)
        {
            _ineractSignals.Clear();
            _ineractSignals.Add(building.GetComponent<BuildingInteractSignal>());
            InteractSignalAdded?.Invoke();
        }
    }

    public void OnBuildingFactoryTypeIDChanged(string typeId)
    {
        _currectFactoryTypeID = typeId;
        FactoryTypeIdChanged?.Invoke();
    }
}
