using CountrySystem;
using Interfaces;

namespace Data
{
    public class PersistantData : IPersistantData
    {
        public CountryData PlayerData { get; set; }
    }
}
