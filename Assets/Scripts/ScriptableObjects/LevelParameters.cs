﻿using Gameplay.TowerLogic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelParameters", menuName = "ScriptableObjects/LevelParameters", order = 2)]
    public class LevelParameters : ScriptableObject
    {
        [SerializeField] private CollectableTower _towerPrefab;
        public CollectableTower TowerPrefab => _towerPrefab;
        
        [SerializeField] private int _towersCount;
        public int TowersCount => _towersCount;
    }
}
