using BuildingSystem;
using System.Collections.Generic;

public static class BuildingsConstants
{
    public static readonly Dictionary<string, Building> BuildingsKey;

    public static void Initzialize()
    {
        BuildingsKey.Add(nameof(TestBuilding), new TestBuilding());
        BuildingsKey.Add(nameof(Test2Building), new Test2Building());
    }
}

