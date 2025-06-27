using BuildingSystem.Configs;
using BuildingSystem.Factory;
using Interfaces;

public class BuildingsFactories : IInitzializable
{
    private TestBuildingsFactory _testFactory;

    private TestBuildingConfig _testBuildingConfig;

    public TestBuildingsFactory TestFactory => _testFactory;

    public BuildingsFactories(TestBuildingConfig testBuildingConfig)
    {
        _testBuildingConfig = testBuildingConfig;
    }

    public void Initzialize()
    {
        _testFactory = new TestBuildingsFactory(_testBuildingConfig);
    }
}

