using R3;
using UnityEngine;

namespace Data.Proxy
{
    public class BuildingStateProxy
    {
        public int Id { get; }
        public int Level { get; }
        public string TypeId { get; }
        public Vector2Int Size { get; }

        public readonly ReactiveProperty<Vector2Int> Position;

        public BuildingStateProxy(BuildingState buildingState)
        {
            Id = buildingState.Id;
            TypeId = buildingState.TypeId;
            Size = buildingState.Size;
            Position = new ReactiveProperty<Vector2Int>(buildingState.Position);
            Level = buildingState.level;

            Position.Skip(1).Subscribe(value => buildingState.Position = value);
        }

    }
}
