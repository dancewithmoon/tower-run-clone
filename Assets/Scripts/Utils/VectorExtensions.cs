using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static float GetDistanceX(Vector3 point1, Vector3 point2)
    {
        return Mathf.Abs(point2.x - point1.x);
    }
    
    public static float GetDistanceY(Vector3 point1, Vector3 point2)
    {
        return Mathf.Abs(point2.y - point1.y);
    }
    
    public static float GetDistanceZ(Vector3 point1, Vector3 point2)
    {
        return Mathf.Abs(point2.z - point1.z);
    }
    
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
