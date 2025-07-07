using Interfaces;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingsInitzialize : IInitzializable
    {
        private const string BUILDINGS_KEY = "country_data";
        private const string BUILDINGSINIT_KEY = "countryInit_key";

        public void Initzialize()
        {
            var dataIsInitzialized = PlayerPrefs.GetInt(BUILDINGSINIT_KEY, 0);

            if (dataIsInitzialized == 1)
            {
            }
            else
            {
                
                

                PlayerPrefs.SetInt(BUILDINGSINIT_KEY, 1);
            }
        }
    }
}
