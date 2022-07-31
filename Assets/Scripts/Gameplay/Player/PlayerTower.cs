using System.Collections.Generic;
using System.Linq;
using Gameplay.TowerLogic;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerTower : MonoBehaviour
    {
        [SerializeField] private Human _startHuman;
        [SerializeField] private BoxCollider _checkCollider;
        [SerializeField] private float _fixationMaxDistance;
        [SerializeField] private Transform _footsPoint;
        [SerializeField] private Jumper _jumper;
        
        private readonly List<Human> _humans = new List<Human>();

        private void Start()
        {
            _humans.Add(Instantiate(_startHuman, transform.position, Quaternion.identity, transform));
            UpdateFootsPointPosition();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Human human) && _humans.Contains(human) == false)
            {
                var touchedTower = human.GetComponentInParent<CollectableTower>();
                if(touchedTower == null)
                    return;
                
                TouchCollectableTower(touchedTower);
            }
        }

        private void TouchCollectableTower(CollectableTower touchedTower)
        {
            _jumper.Stop();
            CollectHumansFromTower(touchedTower);
            touchedTower.Break();
        }
        
        private void CollectHumansFromTower(CollectableTower collectableTower)
        {
            List<Human> humansToCollect = collectableTower.PullOutHumansToCollect(_footsPoint, _fixationMaxDistance);
            if (humansToCollect != null)
            {
                InsertHumans(humansToCollect);
            }
        }
        
        private void InsertHumans(List<Human> toInsert)
        {
            transform.position = toInsert.First().FixationPoint.position - _footsPoint.localPosition;
            for (int i = toInsert.Count - 1; i >= 0; i--)
            {
                InsertHuman(toInsert[i]);
            }
            UpdateFootsPointPosition();
        }

        private void InsertHuman(Human human)
        {
            _humans.Insert(0, human);
            SetHumanPosition(human);
        }
        
        private void SetHumanPosition(Human human)
        {
            human.transform.parent = transform;
            human.transform.localPosition = Vector3.Scale(human.transform.localPosition, Vector3.up);
            human.transform.localRotation = Quaternion.identity;
        }

        private void UpdateFootsPointPosition()
        {
            _footsPoint.position = _humans.First().FootsPoint.position;
            _checkCollider.center = _footsPoint.localPosition;
        }
    }
}
