using Constants;
using Infrastructure;
using SceneManagement;
using UnityEngine;
using UserInput;

[DefaultExecutionOrder(-50)]
public class MainRoot : MonoBehaviour
{
    private ISceneLoader _sceneLoader;
    
    private void Awake()
    {
        Application.targetFrameRate = 120;
        
        ServiceLocator.RegisterSingle<IInputService>(new StandaloneInputService());
        
        _sceneLoader = new SimpleSceneLoader();
        ServiceLocator.RegisterSingle<ISceneLoader>(_sceneLoader);
    }

    private void Start()
    {
        _sceneLoader.LoadScene(Scenes.SampleScene);
    }
}