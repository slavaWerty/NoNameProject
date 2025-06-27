using BuildingSystem.Factory;
using Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BuildingSystem 
{
    public class BuildingsGrid : IInitzializable
    {
        public Action GridChanged;

        private Camera _mainCamera;
        private Vector2Int _gridSize = new Vector2Int(10, 10);
        private Building[,] _grid;
        private Building _flyingBuilding;
        private List<Building> _buildings;

        public List<Building> Buildings => _buildings;

        public BuildingsGrid(Camera mainCamera, Vector2Int gridSize)
        {
            _buildings = new List<Building>();
            _mainCamera = mainCamera;
            _gridSize = gridSize;
        }

        public void Initzialize()
        {
            _grid = new Building[_gridSize.x, _gridSize.y];
            _mainCamera = Camera.main;
        }

        public void StartPlacingBuilding(BuildingsFactory buildingFactory)
        {
            if (_flyingBuilding != null)
            {
                GameObject.Destroy(_flyingBuilding.gameObject);
            }

            _flyingBuilding = buildingFactory.Build();
        }

        public void Update()
        {
            Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (_flyingBuilding != null)
            {
                int x = Mathf.RoundToInt(mouseWorldPosition.x);
                int y = Mathf.RoundToInt(mouseWorldPosition.y);

                _flyingBuilding.transform.position = new Vector2(x, y);

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
                    Debug.Log("building postavili");
                }
            }

            _flyingBuilding.SetNormal();
            _flyingBuilding = null;

            GridChanged?.Invoke();
        }
    }
}



