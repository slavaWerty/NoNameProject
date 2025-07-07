using Interfaces;
using System;
using UnityEngine;

namespace CountrySystem
{
    public class Country : IInitzializable
    {
        public Action DataChanged;

        private const string COUNTRY_KEY = "country_data";
        private const string COUNTRYINIT_KEY = "countryInit_key";

        private CountryConfig _config;

        private CountryData _data;

        public CountryData Data => _data;

        public Country(CountryConfig countryConfig)
        {
            _config = countryConfig;
        }

        public void Initzialize()
        {
            // Надо переделать здесь с условием геймстейт

            var dataIsInitzialized = PlayerPrefs.GetInt(COUNTRYINIT_KEY, 0);

            if (dataIsInitzialized == 1)
            {
            }
            else
            {
                _data = new CountryData(_config.CountFillagers,
                _config.CountIrons, _config.CountTrees);

                PlayerPrefs.SetInt(COUNTRYINIT_KEY, 1);
            }
        }

        public void ChangeData(int number)
        {
            _data.CountFillagers += number;

            DataChanged?.Invoke();
        }
    }
}
