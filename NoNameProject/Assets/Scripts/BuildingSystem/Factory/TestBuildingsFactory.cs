using BuildingSystem.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem.Factory
{
    public class TestBuildingsFactory : BuildingsFactory
    {
        private TestBuildingConfig _config;
        private List<TestBuilding> _testBuildings;

        public List<TestBuilding> TestBuildings => _testBuildings;

        public TestBuildingsFactory(TestBuildingConfig config)
        {
            _config = config;
            _testBuildings = new List<TestBuilding>();
        }

        public override Building Build()
        {
            var prefap = Resources.Load<GameObject>("Prefaps/Circle");
            var go = GameObject.Instantiate(prefap);
            var building = go.AddComponent<TestBuilding>();
            var renderer = go.GetComponent<SpriteRenderer>();
            go.AddComponent<BuildingInteractSignal>();
            building.Initzialize(renderer, _config.Size);
            _testBuildings.Add(building);
            return building;
        }
    }
}
