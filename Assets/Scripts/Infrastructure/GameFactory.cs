using UnityEngine;

namespace Infrastructure
{
    public static class GameFactory
    {
        public static GameObject Instantiate(string path) => 
            Object.Instantiate(Resources.Load<GameObject>(path));

        public static GameObject Instantiate(string path, Vector3 at) =>
            Object.Instantiate(Resources.Load<GameObject>(path), at, Quaternion.identity);
    }
}