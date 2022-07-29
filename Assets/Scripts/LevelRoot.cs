using Gameplay.Generators;
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
    [SerializeField] private LevelParameters _levelParameters;
    private ILevelGenerator _levelGenerator;
    
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            InitializeInEditorMode();
        }
        _levelGenerator = new LevelGenerator(_pathCreator);

        ServiceLocator.RegisterSingle<PathCreator>(_pathCreator);
        ServiceLocator.RegisterSingle<PlayerParameters>(_playerParameters);
        ServiceLocator.RegisterSingle<ILevelGenerator>(_levelGenerator);
    }

    private void Start()
    {
        _levelGenerator.Generate(_levelParameters);
    }

    private static void InitializeInEditorMode()
    {
        if (ServiceLocator.Single<IInputService>() == null)
        {
            ServiceLocator.RegisterSingle<IInputService>(new StandaloneInputService());
        }
    }
}
