using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    private const int warriorsCountLimit = 5;

    [SerializeField] private Warrior _warriorPrefab;
    [SerializeField] private UIController _uIController;

    private readonly List<Warrior> _warriors = new List<Warrior>();
    private Vector3 _position;
    private GameState _gameState = GameState.None;

    public IEnumerable<Warrior> Warriors => _warriors;

    public int CurrentWarriorsCount
    {
        get
        {
            WarriorsCountChanged?.Invoke(_warriors.Count);
            return _warriors.Count;
        }
    }

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

    public int WarriorsCountLimit => warriorsCountLimit;

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
            _gameState = GameState.Setup;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            _gameState = GameState.None;
        }
        if (CurrentWarriorsCount > 0)
        {
            CurrentGameState = GameState.Game;
        }
    }

    public void InstantiateWarrior(Vector3 position, Quaternion rotation)
    {
        if (CurrentWarriorsCount < WarriorsCountLimit)
        {
            Warrior warrior = Instantiate(_warriorPrefab, position, rotation);
            _warriors.Add(warrior);

            warrior.Initialize(this);
        }
    }

    public void RemoveWarrior(Warrior warrior)
    {
        _warriors.Remove(warrior);

        if (CurrentWarriorsCount <= 0)
        {
            _gameState = GameState.Setup;
        }
    }

    public void InstantiateWarriorOnSimulate()
    {
        _position = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(10, 20));
        InstantiateWarrior(_position, Quaternion.identity);
    }

    public void Restart()
    {
        SceneManager.LoadScene("AR_DeathMatch");
    }
}
