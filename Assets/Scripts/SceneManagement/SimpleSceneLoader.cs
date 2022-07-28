using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SimpleSceneLoader : ISceneLoader
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
