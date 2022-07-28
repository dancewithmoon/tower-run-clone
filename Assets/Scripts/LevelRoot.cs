using Infrastructure;
using PathCreation;
using ScriptableObjects;
using UnityEngine;
using UserInput;

[DefaultExecutionOrder(-49)]
public class LevelRoot : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private PlayerParameters _playerParameters;
    
    private void Awake()
    {
        ServiceLocator.RegisterSingle<PathCreator>(_pathCreator);
        ServiceLocator.RegisterSingle<PlayerParameters>(_playerParameters);
        
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            InitializeInEditorMode();
        }
    }

    private static void InitializeInEditorMode()
    {
        if (ServiceLocator.Single<IInputService>() == null)
        {
            ServiceLocator.RegisterSingle<IInputService>(new StandaloneInputService());
        }
    }
}
