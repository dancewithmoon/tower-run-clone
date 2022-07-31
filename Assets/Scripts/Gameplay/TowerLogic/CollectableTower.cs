using System.Collections.Generic;
using UnityEngine;
using Utils.Structures;

namespace Gameplay.TowerLogic
{
    public class CollectableTower : MonoBehaviour
    {
        [SerializeField] private MinMaxInt _humanInTowerRange; 
        [SerializeField] private Human[] _humanPrefabs;

        private readonly List<Human> _humansInTower = new List<Human>();

        private void Start()
        {
            int humansInTowerCount = Random.Range(_humanInTowerRange.Min, _humanInTowerRange.Max);
            SpawnHumans(humansInTowerCount);
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

        public List<Human> PullOutHumansToCollect(Transform footsPoint, float fixationMaxDistance)
        {
            for (int i = 0; i < _humansInTower.Count; i++)
            {
                float distanceBetweenPoints =
                    VectorExtensions.GetDistanceY(footsPoint.position, _humansInTower[i].FixationPoint.position);

                if (distanceBetweenPoints > fixationMaxDistance)
                    continue;

                List<Human> collectedHumans = _humansInTower.GetRange(0, i + 1);
                _humansInTower.RemoveRange(0, i + 1);
                return collectedHumans;
            }
            return null;
        }

        public void Break()
        {
            foreach (Human human in _humansInTower)
            {
                human.transform.parent = null;
                human.Animator.applyRootMotion = false;
                
                var humanRigidbody = human.gameObject.AddComponent<Rigidbody>();
                humanRigidbody.AddForce(
                    new Vector3(GetRandomForce(), 0, GetRandomForce()), ForceMode.Force);
                humanRigidbody.AddTorque(GetRandomForce(),GetRandomForce(), GetRandomForce());
                Destroy(human.gameObject, 1f);
            }
            Destroy(gameObject, 1f);
        }

        private float GetRandomForce()
        {
            return Random.Range(-300, 300);
        }
    }
}
