using UnityEngine;

public static class MeshExtensions
{
    public static Vector3 GetRealSize(this MeshFilter obj)
    {
        return new Vector3(
            obj.transform.localScale.x * obj.sharedMesh.bounds.size.x,
            obj.transform.localScale.y * obj.sharedMesh.bounds.size.y,
            obj.transform.localScale.z * obj.sharedMesh.bounds.size.z);
    }
}

