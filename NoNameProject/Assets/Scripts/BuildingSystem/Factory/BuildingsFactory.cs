using BuildingSystem;

public abstract class BuildingsFactory : Factory
{
    public abstract Building Build(int level);
}

