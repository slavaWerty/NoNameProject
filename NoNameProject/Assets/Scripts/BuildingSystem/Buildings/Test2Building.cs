using BuildingSystem;
using UnityEngine;

public class Test2Building : Building
{
    public override string BuildingKey { get => nameof(Test2Building); }

    public override void SetNormal()
    {
        MainRenderer.material.color = Color.white;
    }

    public override void SetTransparent(bool available)
    {
        if (available)
            MainRenderer.material.color = Color.green;
        else
            MainRenderer.material.color = Color.yellow;
    }
}

