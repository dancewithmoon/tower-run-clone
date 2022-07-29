using PathCreation;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay.Generators
{
    public class LevelGenerator : ILevelGenerator
    {
        private readonly PathCreator _pathCreator;
        
        public LevelGenerator(PathCreator pathCreator)
        {
            _pathCreator = pathCreator;
        }

        public void Generate(LevelParameters parameters)
        {
            float pathLength = _pathCreator.path.length;
            float distanceBetweenTowers = pathLength / (parameters.TowersCount + 1);

            float distanceTraveled = 0;
            Vector3 spawnPoint;
            for (int i = 0; i < parameters.TowersCount; i++)
            {
                distanceTraveled += distanceBetweenTowers;
                spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
                Object.Instantiate(parameters.TowerPrefab, spawnPoint, Quaternion.identity);
            }
        }
    }
}
