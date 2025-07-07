using UnityEngine;

public abstract class BuildingConfig : ScriptableObject
{
    [SerializeField] private Vector2Int _size;

    public Vector2Int Size => _size;
}

