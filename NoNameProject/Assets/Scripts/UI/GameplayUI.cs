using CountrySystem;
using Interfaces;
using System;
using TMPro;
using VContainer;

namespace UI
{
    public class GameplayUI : IInitzializable, IDisposable
    {
        private TextView _testText;
        private Country _country;

        [Inject]
        public GameplayUI(TextMeshProUGUI text, Country country)
        {
            _testText = new TextView(text);
            _country = country;
        }

        public void Initzialize()
        {
            _country.DataChanged += OnDataChanged;
        }

        public void OnDataChanged()
        {
            _testText.TextChange(_country.Data.CountFillagers.ToString());
        }

        public void Dispose()
        {
            _country.DataChanged -= OnDataChanged;
        }
    }
}
