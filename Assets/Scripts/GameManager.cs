using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    private const int WarriorsCountLimit = 5;

    [SerializeField] private Warrior _warriorPrefab;
    [SerializeField] private UIController _uIController;

    private readonly List<Warrior> _warriors = new List<Warrior>();
    private Vector3 _position;
    private GameState _gameState = GameState.None;

    public IEnumerable<Warrior> Warriors => _warriors;

    public int CurrentWarriorsCount => _warriors.Count;

    public GameState CurrentGameState
    {
        get
        {
            return _gameState;
        }
        private set
        {
            _gameState = value;
            GameStateChanged?.Invoke(_gameState);
        }
    }

    public int WarriorsLimit => WarriorsCountLimit;

    public event Action<GameState> GameStateChanged;
    public event Action<int> WarriorsCountChanged;

    private void Start()
    {
        _uIController.Initialize(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InstantiateWarriorOnSimulate();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CurrentGameState = GameState.Setup;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            CurrentGameState = GameState.None;
        }
        if (CurrentWarriorsCount > 0)
        {
            CurrentGameState = GameState.Game;
        }
    }

    public void InstantiateWarrior(Vector3 position, Quaternion rotation)
    {
        if (CurrentWarriorsCount < WarriorsLimit)
        {
            Warrior warrior = Instantiate(_warriorPrefab, position, rotation);
            _warriors.Add(warrior);

            warrior.Initialize(this);
            WarriorsCountChanged?.Invoke(CurrentWarriorsCount);
        }
    }

    public void RemoveWarrior(Warrior warrior)
    {
        _warriors.Remove(warrior);
        WarriorsCountChanged?.Invoke(CurrentWarriorsCount);

        if (CurrentWarriorsCount <= 0)
        {
            CurrentGameState = GameState.Setup;
        }
    }

    public void InstantiateWarriorOnSimulate()
    {
        _position = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(10, 20));
        InstantiateWarrior(_position, Quaternion.identity);
    }

    public void Restart()
    {
        SceneManager.LoadScene("AugmentedRealityDeathMatch");
    }

    public void EnableSetupMode()
    {
        CurrentGameState = GameState.Setup;
    }
}
