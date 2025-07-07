using ObservableCollections;
using R3;
using System.Linq;

namespace Data.Proxy
{
    public class GameStateProxy
    {
        public ObservableList<BuildingStateProxy> BuildingStates { get; } = new();

        private int _entityId = 0;

        public GameStateProxy(GameState state)
        {
            state.BuildingStates.ForEach(buildingOrigin => BuildingStates.Add(new BuildingStateProxy(buildingOrigin)));

            BuildingStates.ObserveAdd().Subscribe(e =>
            {
                var addedBuildingEntity = e.Value;
                state.BuildingStates.Add(new BuildingState
                {
                    Id = addedBuildingEntity.Id,
                    TypeId = addedBuildingEntity.TypeId,
                    Size = addedBuildingEntity.Size,
                    Position = addedBuildingEntity.Position.Value,
                    level = addedBuildingEntity.Level,
                });
            });

            BuildingStates.ObserveRemove().Subscribe(e =>
            {
                var removedBuildingEntity = e.Value;
                var removedBuildingState = state.BuildingStates.FirstOrDefault(b => b.Id == removedBuildingEntity.Id);
                state.BuildingStates.Remove(removedBuildingState);
            });
        }

        public int GetEntityId()
        {
            _entityId += 1;
            return _entityId;
        }
    }
}
