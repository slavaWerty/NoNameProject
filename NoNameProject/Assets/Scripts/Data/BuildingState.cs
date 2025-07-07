using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class BuildingState
    {
        public int Id;
        public int level;
        public string TypeId;
        public Vector2Int Size;
        public Vector2Int Position;
    }
}
