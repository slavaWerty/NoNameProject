using CountrySystem;
using Interfaces;
using System;
using System.Collections.Generic;
using TMPro;
using VContainer;

namespace UI
{
    public class GameplayUI : IInitzializable, IDisposable
    {
        private List<ChangeBuildingsStateUI> _changeBuildingsStateUIs;
        private BuildingsService _buildingsService;
        private BuildingConfig _circleConfig;
        private BuildingConfig _capsuleConfig;

        private TextView _testText;
        private Country _country;

        [Inject]
        public GameplayUI(TextMeshProUGUI text, Country country,
            List<ChangeBuildingsStateUI> changeBuildingsStateUIs,
            BuildingsService buildingsService, BuildingConfig circleConfig,
            BuildingConfig capsuleConfig)
        {
            _testText = new TextView(text);
            _country = country;
            _buildingsService = buildingsService;
            _changeBuildingsStateUIs = changeBuildingsStateUIs;
            _circleConfig = circleConfig;
            _capsuleConfig = capsuleConfig;
        }

        public void Initzialize()
        {
            // В целом надо такую регистрацию,
            // где надо все по проекту помнить
            // куда-то отдельно заисывать

            _changeBuildingsStateUIs[0].Initzialize(nameof(TestBuilding));
            _changeBuildingsStateUIs[1].Initzialize(nameof(Test2Building));

            _country.DataChanged += OnDataChanged;
            foreach (var changeBuildingsStateUI in _changeBuildingsStateUIs)
                changeBuildingsStateUI.BuildingFactoryChanged += _buildingsService.OnBuildingFactoryTypeIDChanged;
        }

        public void OnDataChanged()
        {
            _testText.TextChange(_country.Data.CountFillagers.ToString());
        }

        public void Dispose()
        {
            _country.DataChanged -= OnDataChanged;
            foreach (var changeBuildingsStateUI in _changeBuildingsStateUIs)
                changeBuildingsStateUI.BuildingFactoryChanged -= _buildingsService.OnBuildingFactoryTypeIDChanged;
        }
    }
}
