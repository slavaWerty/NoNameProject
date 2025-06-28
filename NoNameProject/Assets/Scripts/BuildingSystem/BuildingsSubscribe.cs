using BuildingSystem;
using BuildingSystem.Factory;
using Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsSubscribe : IInitzializable, IDisposable
{
    public Action InteractSignalAdded;

    private BuildingsGrid _grid;
    private List<Button> _buttons;
    private BuildingsFactories _factories;
    private List<InteractSignal> _ineractSignals;

    public List<InteractSignal> InteractSignals => _ineractSignals;

    public BuildingsSubscribe(BuildingsGrid grid,
        List<Button> buttons, BuildingsFactories factories)
    {
        _ineractSignals = new List<InteractSignal>();
        _grid = grid;
        _buttons = buttons;
        _factories = factories;
    }

    private void PlaseBuilding(BuildingsFactory buildingFactory)
    {
        _grid.StartPlacingBuilding(buildingFactory);
    }

    public void Update()
    {
        _grid.Update();
    }

    public void Initzialize()
    {
        _grid.Initzialize();
        _factories.Initzialize();

        _buttons[0].onClick.AddListener(() =>
        {
            PlaseBuilding(_factories.TestFactory);
        });

        _grid.GridChanged += OnGridChanged;
    }

    public void Dispose()
    {
        _buttons[0].onClick.RemoveListener(() =>
        {
            PlaseBuilding(_factories.TestFactory);
        });

        _grid.GridChanged -= OnGridChanged;
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
}
