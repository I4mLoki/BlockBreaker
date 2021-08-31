using System;
using DataConfig;
using Gameplay;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private BaseLevelList levelList;

    public static GameplayManager Instance { get; private set; }

    private PlayerActions _playerActions;
    private EnemiesActions _enemiesActions;

    private BaseLevel _level;
    private bool _levelLoaded;

    private enum GameState
    {
        PLAYER_TURN,
        ENEMY_TURN,
        PAUSED
    }

    private GameState CurrentGameState;

    private void Awake()
    {
        Instance = this;

        _playerActions = GetComponent<PlayerActions>();
        _enemiesActions = GetComponent<EnemiesActions>();
    }

    private void Start()
    {
        GamePaused = true;
        
        _playerActions.LoadComponents();
        _enemiesActions.LoadComponents();
    }

    public void StartGameplay(int desiredLevel)
    {
        _level = levelList.LevelList.Find(lvl => lvl.LevelNumber == desiredLevel);

        if (_level == null)
        {
            Debug.LogError($"Unable to find level {desiredLevel} in BaseLevelList, gameplay aborted.");
            return;
        }

        _enemiesActions.InitialEnemiesTurn(_level);
    }

    public void SetPlayerTurn()
    {
        CurrentGameState = GameState.PLAYER_TURN;
    }
    
    public bool IsPlayerTurn()
    {
        return CurrentGameState == GameState.PLAYER_TURN;
    }

    public bool EnemiesTurn
    {
        get => CurrentGameState == GameState.ENEMY_TURN;
        set => CurrentGameState = GameState.ENEMY_TURN;
    }

    public bool GamePaused
    {
        get => CurrentGameState == GameState.PAUSED;
        set => CurrentGameState = GameState.PAUSED;
    }
}