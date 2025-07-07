using BuildingSystem;
using BuildingSystem.Configs;
using BuildingSystem.Factory;
using CountrySystem;
using System.Collections;
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
    [SerializeField] private List<ChangeBuildingsStateUI> _changesBuildingsStateUI;
    [Space(10)]
    [SerializeField] private TestBuildingConfig _testCircleBuildingConfig;
    [SerializeField] private TestBuildingConfig _testCapsuleBuildingConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        var playerPrefsDataProvider = new PlayerPrefsGameStateProvider();
        builder.RegisterInstance<IGameStateProvider>(playerPrefsDataProvider);

        var buildingsService = BuildingsSystemRegister(builder, playerPrefsDataProvider);

        CameraMoveRegister(builder);
        var country = CountryRegister(builder);
        InteractSystemRegister(builder, buildingsService);
        RegisterGameplayUI(builder, country, buildingsService);

        builder.RegisterEntryPoint<GameplayEntryPoint>();
    }

    private BuildingsService BuildingsSystemRegister(IContainerBuilder builder, IGameStateProvider gameStateProvider)
    {
        Dictionary<string, BuildingsFactory> dictionary = FactoryDictionaryRegister();
        var buildingsSpawner = new BuildingsSpawner(_camera, _gridSize, dictionary, gameStateProvider);
        builder.RegisterInstance(buildingsSpawner);
        var buildingSubscribe = new BuildingsService(buildingsSpawner,
            _buildingButtons);
        builder.RegisterInstance(buildingSubscribe);
        return buildingSubscribe;
    }

    private Dictionary<string, BuildingsFactory> FactoryDictionaryRegister()
    {
        Dictionary<string, BuildingsFactory> dictionary = new Dictionary<string, BuildingsFactory>();
        dictionary[nameof(TestBuilding)] = new TestBuildingFactory(_testCapsuleBuildingConfig, _testCircleBuildingConfig);
        dictionary[nameof(Test2Building)] = new Test2BuildingFactory(_testCapsuleBuildingConfig, _testCircleBuildingConfig);
        return dictionary;
    }

    private void InteractSystemRegister(IContainerBuilder builder, BuildingsService buildingSubscribe)
    {
        var uiFactory = new TextUIFactory(_container);
        var inetractSbscribe = new InteractService(uiFactory, buildingSubscribe);
        builder.RegisterInstance(inetractSbscribe);
    }

    private Country CountryRegister(IContainerBuilder builder)
    {
        var country = new Country(_countryConfig);
        builder.RegisterInstance(country);
        return country;
    }

    private void RegisterGameplayUI(IContainerBuilder builder, Country country,
        BuildingsService buildingsService)
    {
        var gameplayUI = new GameplayUI(_fillagersText,
            country, _changesBuildingsStateUI, buildingsService, _testCircleBuildingConfig, _testCapsuleBuildingConfig);
        builder.RegisterInstance(gameplayUI);
    }

    private void CameraMoveRegister(IContainerBuilder builder)
    {
        var cameraMove = new CameraMove(_camera.transform);
        builder.RegisterInstance(cameraMove);
    }
}
