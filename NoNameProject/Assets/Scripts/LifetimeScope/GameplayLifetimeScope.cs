using CountrySystem;
using StorageService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [Header("Property")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CountryConfig _countryConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        CameraMoveRegister(builder);
        CountryRegister(builder);

        builder.RegisterEntryPoint<GameplayEntryPoint>();
    }

    private void CountryRegister(IContainerBuilder builder)
    {
        var storageService = new JsonStorageService();
        var country = new Country(_countryConfig, storageService);
        builder.RegisterInstance(country);
    }

    private void CameraMoveRegister(IContainerBuilder builder)
    {
        var cameraMove = new CameraMove(_cameraTransform);
        builder.RegisterInstance(cameraMove);
    }
}
