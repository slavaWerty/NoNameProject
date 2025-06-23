using CountrySystem;
using VContainer;
using VContainer.Unity;

public class GameplayEntryPoint : IStartable, ITickable
{
    [Inject] private CameraMove _cameraMove;
    [Inject] private Country _country;

    public void Start()
    {
        _cameraMove.Initzialize();
        _country.Initzialize();
    }

    public void Tick()
    {
        _cameraMove.Update();
    }
}

