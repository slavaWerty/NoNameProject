using CountrySystem;
using System;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayEntryPoint : IStartable, ITickable, IDisposable
{
    [Inject] private CameraMove _cameraMove;
    [Inject] private Country _country;
    [Inject] private GameplayUI _ui;
    [Inject] private BuildingsService _buildingsSubscribe;
    [Inject] private InteractService _interactSubscribe;
    [Inject] private IGameStateProvider _gameStateProvider;

    public void Start()
    {
        _ui.Initzialize();
        _cameraMove.Initzialize();
        _country.Initzialize();
        _interactSubscribe.Initzialize();
        _buildingsSubscribe.Initzialize();
        _gameStateProvider.LoadGameState();
    }

    public void Tick()
    {
        _cameraMove.Update();
        _buildingsSubscribe.Update();

        if(Input.GetKeyDown(KeyCode.R))
            _country.ChangeData(5);
    }

    public void Dispose()
    {
        _ui.Dispose();
        _buildingsSubscribe.Dispose();
        _interactSubscribe.Dispose();
    }
}

