using BuildingSystem;
using BuildingSystem.Configs;
using CountrySystem;
using StorageService;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [Header("Property")]
    [SerializeField] private Camera _camera;
    [SerializeField] private CountryConfig _countryConfig;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI _fillagersText;
    [Space(10)]
    [SerializeField] private Transform _container;
    [Space(10)]
    [SerializeField] private Vector2Int _gridSize;
    [Space(10)]
    [SerializeField] private List<Button> _buildingButtons;
    [Space(10)]
    [SerializeField] private TestBuildingConfig _testBuildingConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        BuildingsSystemRegister(builder);

        CameraMoveRegister(builder);
        CountryRegister(builder);

        builder.RegisterEntryPoint<GameplayEntryPoint>();
    }

    private void BuildingsSystemRegister(IContainerBuilder builder)
    {
        var buildingsGrid = new BuildingsGrid(_camera, _gridSize);

        var factories = new BuildingsFactories(_testBuildingConfig);
        var buildingSubscribe = new BuildingsSubscribe(buildingsGrid, _buildingButtons, factories);
        builder.RegisterInstance(buildingSubscribe);

        InteractSystemRegister(builder, buildingSubscribe);
    }

    private void InteractSystemRegister(IContainerBuilder builder, BuildingsSubscribe buildingSubscribe)
    {
        var uiFactory = new TextUIFactory(_container);
        var inetractSbscribe = new InteractSubscribe(uiFactory, buildingSubscribe);
        builder.RegisterInstance(inetractSbscribe);
    }

    private void CountryRegister(IContainerBuilder builder)
    {
        var storageService = new JsonStorageService();
        var country = new Country(_countryConfig, storageService);
        builder.RegisterInstance(country);

        RegisterGameplayUI(builder, country);
    }

    private void RegisterGameplayUI(IContainerBuilder builder, Country country)
    {
        var gameplayUI = new GameplayUI(_fillagersText, country);
        builder.RegisterInstance(gameplayUI);
    }

    private void CameraMoveRegister(IContainerBuilder builder)
    {
        var cameraMove = new CameraMove(_camera.transform);
        builder.RegisterInstance(cameraMove);
    }
}
