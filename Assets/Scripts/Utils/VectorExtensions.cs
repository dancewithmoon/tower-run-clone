using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static T FindNearest<T>(Vector3 position, IEnumerable<T> objects) where T : Component
    {
        float minDistance = Mathf.Infinity;
        T nearest = null;
        foreach (T currentObject in objects)
        {
            float currentDistance = SqrDistance(position, currentObject.transform.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                nearest = currentObject;
            }
        }
        return nearest;
    }

    public static float SqrDistance(Vector3 a, Vector3 b) => (b - a).sqrMagnitude;
}
