using MainGameplay;
using UnityEngine;
using Models.UI;
using Managers;
using Settings;
using UIViews;
using Tools;

// main composition root
public class Entry : MonoBehaviour
{
#pragma warning disable 0649
        
    [SerializeField] private GameplaySettings _settings;
    
    [SerializeField] private BallPositionChecker _ballPositionChecker;
    
    [SerializeField] private GameObject _mainCamera;

    [SerializeField] private BallMovement _ballMovement;

    [SerializeField] private BallBehaviour _ballBehaviour;
    
    [Header("UI")]
    
    [SerializeField] private PauseView _pauseView;

#pragma warning restore
    
    private void Start()
    {
        // creating classes instances which require an existing information or references to each others
        var pieceCreator = new PieceCreator(_settings);
        var levelModelCreator = new LevelModelCreator(pieceCreator, _settings);
        var levelSpawner = new LevelSpawner(_settings);
        var levelBuilder = new LevelBuilder(_settings, levelModelCreator, levelSpawner);
        var cameraMover = new CameraMover(_mainCamera);
        var pauseModel = new PauseManager();

        var gameplayManager = new GameSession(levelBuilder, levelSpawner, pauseModel, _ballPositionChecker,
            _ballBehaviour, _ballMovement, cameraMover);
        gameplayManager.Start();
        
        // Ui initialization

        _pauseView.Init(pauseModel, gameplayManager);
    }
}