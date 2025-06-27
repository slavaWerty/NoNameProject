using Interfaces;
using UnityEngine;

namespace CountrySystem
{
    public class Country : IInitzializable
    {
        private const string COUNTRY_KEY = "country_data";
        private const string COUNTRYINIT_KEY = "countryInit_key";

        private CountryConfig _config;
        private IStorageService _storageService;

        private CountryData _data;

        public Country(CountryConfig countryConfig, IStorageService storageService)
        {
            _config = countryConfig;
            _storageService = storageService;
        }

        public void Initzialize()
        {
            var dataIsInitzialized = PlayerPrefs.GetInt(COUNTRYINIT_KEY, 0);

            if (dataIsInitzialized == 1)
            {
                _storageService.Load<CountryData>(COUNTRY_KEY, e =>
                {
                    _data = e;
                });
            }
            else
            {
                _data = new CountryData(_config.CountFillagers,
                _config.CountIrons, _config.CountTrees);
                _storageService.Save(COUNTRY_KEY, _data);

                PlayerPrefs.SetInt(COUNTRYINIT_KEY, 1);
            }
        }
    }
}
