using CountrySystem;
using Data;

namespace Interfaces
{
    public interface IPersistantData
    {
        public CountryData PlayerData { get; set; }
    }
}
