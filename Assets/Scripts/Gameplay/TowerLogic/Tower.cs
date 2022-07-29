using System.Collections.Generic;
using UnityEngine;
using Utils.Structures;

namespace Gameplay.TowerLogic
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private MinMaxInt _humanInTowerRange; 
        [SerializeField] private Human[] _humanPrefabs;

        private readonly List<Human> _humansInTower = new List<Human>();

        private void Start()
        {
            int humanInTowerCount = Random.Range(_humanInTowerRange.Min, _humanInTowerRange.Max);
            SpawnHumans(humanInTowerCount);
        }

        private void SpawnHumans(int count)
        {
            Vector3 spawnPoint = transform.position;

            for (int i = 0; i < count; i++)
            {
                Human spawnedHuman =
                    Instantiate(_humanPrefabs.GetRandomItem(), spawnPoint, Quaternion.identity, transform);
                spawnPoint = spawnedHuman.FixationPoint.position;
                _humansInTower.Add(spawnedHuman);
            }
        }
    }
}
