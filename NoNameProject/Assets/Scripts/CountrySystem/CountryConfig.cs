using UnityEngine;

namespace CountrySystem
{
    [CreateAssetMenu(fileName = "new CountryConfig", menuName = "Country/CountryConfig")]
    public class CountryConfig : ScriptableObject
    {
        [SerializeField] private int _countFillagers;
        [SerializeField] private int _countIrons;
        [SerializeField] private int _countTrees;

        public int CountFillagers => _countFillagers;
        public int CountIrons => _countIrons;
        public int CountTrees => _countTrees;
    }
}
