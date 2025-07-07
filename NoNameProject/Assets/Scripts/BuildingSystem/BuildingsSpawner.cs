using Data.Proxy;
using Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using R3;
using ObservableCollections;
using Data;

namespace BuildingSystem
{
    public class BuildingsSpawner : IInitzializable
    {
        private Dictionary<string, BuildingsFactory> Factories;

        public Action GridChanged;

        private Camera _mainCamera;
        private Vector2Int _gridSize = new Vector2Int(10, 10);
        private Building[,] _grid;
        private Building _flyingBuilding;
        private List<Building> _buildings;
        private IGameStateProvider _gameStateProvider;
        private int _levelCurrentFlyingBuilding;

        public List<Building> Buildings => _buildings;

        public BuildingsSpawner(Camera mainCamera,
            Vector2Int gridSize, 
            Dictionary<string, BuildingsFactory> factories,
            IGameStateProvider gameStateProvider)
        {
            Factories = factories;

            _buildings = new List<Building>();
            _mainCamera = mainCamera;
            _gridSize = gridSize;
            _gameStateProvider = gameStateProvider;
        }

        public void Initzialize()
        {
            _grid = new Building[_gridSize.x, _gridSize.y];
            _mainCamera = Camera.main;
            _gameStateProvider.LoadGameState();
            PlaseBuildings();
        }

        public void StartPlacingBuilding(string typeIdPlacingBuilding, int level)
        {
            if (_flyingBuilding != null)
                GameObject.Destroy(_flyingBuilding.gameObject);

            _flyingBuilding = Factories[typeIdPlacingBuilding].Build(level);
            _levelCurrentFlyingBuilding = level;
            Debug.Log($"BuildingSpawner{level}");
        }

        public void Update()
        {
            UnityEngine.Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (_flyingBuilding != null)
            {
                int x = Mathf.RoundToInt(mouseWorldPosition.x);
                int y = Mathf.RoundToInt(mouseWorldPosition.y);

                _flyingBuilding.transform.position = new UnityEngine.Vector2(x, y);

                var available = true;

                if (x < 0 || x > _gridSize.x - _flyingBuilding.Size.x) available = false;
                if (y < 0 || y > _gridSize.y - _flyingBuilding.Size.y) available = false;
                if (available && IsPlaceTaken(x, y)) available = false;

                _flyingBuilding.SetTransparent(available);

                if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
                {
                    GameObject.Destroy(_flyingBuilding.gameObject);
                    _flyingBuilding = null;
                }

                if (Input.GetMouseButtonDown(1) && available)
                {
                    PlaceFlyingBuilding(x, y);
                    GameObject.Destroy(_flyingBuilding);
                }

            }
        }

        private bool IsPlaceTaken(int placeX, int placeY)
        {
            for (int x = 0; x < _flyingBuilding.Size.x; x++)
            {
                for (int y = 0; y < _flyingBuilding.Size.y; y++)
                {
                    if (_grid[placeX + x, placeY + y] != null) return true;
                }
            }

            return false;
        }

        private void PlaceFlyingBuilding(int placeX, int placeY)
        {
            for (int x = 0; x < _flyingBuilding.Size.x; x++)
            {
                for (int y = 0; y < _flyingBuilding.Size.y; y++)
                {
                    _grid[placeX + x, placeY + y] = _flyingBuilding;
                    _buildings.Add(_flyingBuilding);
                    CreateBuildingStateProxy();
                }
            }

            _flyingBuilding.SetNormal();
            _flyingBuilding = null;

            GridChanged?.Invoke();
        }

        private void CreateBuildingStateProxy()
        {
            Debug.Log(_levelCurrentFlyingBuilding);
            var buildingState = new BuildingState()
            {
                Id = _gameStateProvider.GameState.GetEntityId(),
                TypeId = _flyingBuilding.TypeId,
                Size = _flyingBuilding.Size,
                Position = new Vector2Int((int)_flyingBuilding.transform.position.x,
                (int)_flyingBuilding.transform.position.y),
                level = _levelCurrentFlyingBuilding
            };
            _gameStateProvider.GameState.BuildingStates.Add(new BuildingStateProxy(buildingState));
            _gameStateProvider.SaveGameState();
        }

        private void PlaseBuildings()
        {
            var buildingsStateProxies = _gameStateProvider.GameState.BuildingStates;

            foreach (var buildingStateProxy in buildingsStateProxies)
            {
                if(Factories.TryGetValue(buildingStateProxy.TypeId, out BuildingsFactory factory))
                {
                    var building = factory.Build(buildingStateProxy.Level);
                    building.gameObject.transform.position =(Vector2) buildingStateProxy.Position.Value;
                }
            }
        }
    }
}



