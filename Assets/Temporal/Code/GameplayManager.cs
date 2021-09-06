using System;
using Code.DataConfig.BaseObjects;
using Code.DataConfig.DataLists;
using DataConfig;
using Gameplay;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private BaseLevelList levelList;

    public static GameplayManager Instance { get; private set; }

    public float CellSize { get; private set; }
    public int VisibleColumns { get; private set; }
    public int VisibleRows { get; private set; }

    private GameplayGrid _gameplayGrid;
    private PlayerActions _playerActions;
    private EnemiesActions _enemiesActions;

    private BaseLevel _level;
    private bool _levelLoaded;

    private int _visibleRows = 11;
    private float _cellSize;

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

        _gameplayGrid = GetComponent<GameplayGrid>();
        _playerActions = GetComponent<PlayerActions>();
        _enemiesActions = GetComponent<EnemiesActions>();
    }

    private void Start()
    {
        SetPaused();

        _playerActions.LoadComponents();
        _enemiesActions.LoadComponents();
        
        _playerActions.PlayerTurn();
    }

    public void StartGameplay(int desiredLevel)
    {
        _level = levelList.List.Find(lvl => lvl.levelNumber == desiredLevel);

        if (_level == null)
        {
            Debug.LogError($"Unable to find level {desiredLevel} in BaseLevelList, gameplay aborted.");
            return;
        }

        _gameplayGrid.Setup(_level, SetGameplayParams);

        CurrentGameState = GameState.ENEMY_TURN;
        _enemiesActions.InitialEnemiesTurn(_level);
    }

    private void SetGameplayParams(float cellSize, int visibleColumns, int visibleRows)
    {
        CellSize = cellSize;
        VisibleColumns = visibleColumns;
        VisibleRows = visibleRows;
    }

    public void SetPlayerTurn()
    {
        Debug.Log("Player turn");
        CurrentGameState = GameState.PLAYER_TURN;
        _playerActions.PlayerTurn();
    }

    public bool IsPlayerTurn()
    {
        return CurrentGameState == GameState.PLAYER_TURN;
    }

    public void SetEnemiesTurn()
    {
        Debug.Log("Enemies turn");
        // TODO Test proposal, remove player turn and uncomment
        _playerActions.PlayerTurn();
        // CurrentGameState = GameState.ENEMY_TURN;
        // _enemiesActions.EnemiesTurn();
    }

    public bool IsEnemiesTurn()
    {
        return CurrentGameState == GameState.ENEMY_TURN;
    }

    public void SetPaused()
    {
        CurrentGameState = GameState.PAUSED;
    }

    public bool IsPaused()
    {
        return CurrentGameState == GameState.PAUSED;
    }
}