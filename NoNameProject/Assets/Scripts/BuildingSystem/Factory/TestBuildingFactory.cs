using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem.Factory
{
    public class TestBuildingFactory : BuildingsFactory
    {
        private BuildingConfig _capsuleConfig;
        private BuildingConfig _circleConfig;

        private List<Building> _testBuildings;

        public List<Building> TestBuildings => _testBuildings;

        public TestBuildingFactory(BuildingConfig capsuleConfig, BuildingConfig circleConfig)
        {
            _capsuleConfig = capsuleConfig;
            _circleConfig = circleConfig;
            _testBuildings = new List<Building>();
        }

        public override Building Build(int level)
        {
            if (level == 1)
                return BuildFirst();
            else if (level == 2)
                return BuildSecond();
            return null;
        }

        public Building BuildFirst()
        {
            var name = "Circle";
            var prefap = Resources.Load<GameObject>($"Prefaps/{name}");
            var go = GameObject.Instantiate(prefap);
            var building = go.AddComponent<TestBuilding>();
            building.TypeId = nameof(TestBuilding);
            building.Level = 1;
            var renderer = go.GetComponent<SpriteRenderer>();
            go.AddComponent<BuildingInteractSignal>();
            building.Initzialize(renderer, _circleConfig.Size);
            _testBuildings.Add(building);
            return building;
        }

        public Building BuildSecond()
        {
            var name = "Capsule";
            var prefap = Resources.Load<GameObject>($"Prefaps/{name}");
            var go = GameObject.Instantiate(prefap);
            var building = go.AddComponent<TestBuilding>();
            building.TypeId = nameof(TestBuilding);
            building.Level = 2;
            var renderer = go.GetComponent<SpriteRenderer>();
            go.AddComponent<BuildingInteractSignal>();
            building.Initzialize(renderer, _capsuleConfig.Size);
            _testBuildings.Add(building);
            return building;
        }
    }
}
