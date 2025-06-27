using Newtonsoft.Json;

namespace CountrySystem
{
    public class CountryData
    {
        public int CountFillagers;
        public int CountIrons;
        public int CountTrees;

        [JsonConstructor]
        public CountryData(int countFillagers, int countIrons, int countTrees)
        {
            CountFillagers = countFillagers;
            CountIrons = countIrons;
            CountTrees = countTrees;
        }
    }
}
