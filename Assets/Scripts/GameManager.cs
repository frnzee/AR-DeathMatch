using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public partial class GameManager : MonoBehaviour
{
    private const int WarriorsCountLimit = 5;

    [SerializeField] private Warrior _warriorPrefab;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _setupModeText;
    [SerializeField] private GameObject _destroyModeText;
    [SerializeField] private TextMeshProUGUI _warriorsAmountText;

    private readonly List<Warrior> _warriors = new List<Warrior>();
    private int CurrentWarriorsCount => _warriors.Count;
    private Vector3 _position;

    public IEnumerable<Warrior> Warriors => _warriors;
    public GameState CurrentGameState
    {
        get; private set;
    }

    private void Start()
    {
        CurrentGameState = GameState.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InstantiateWarriorOnSimulate();
        }

        if (CurrentGameState == GameState.Game)
        {
            DisableGUI();
        }
        else
        {
            EnableGUI();
        }
    }

    private void EnableGUI()
    {
        _gameUI.SetActive(true);
    }

    private void DisableGUI()
    {
        _gameUI.SetActive(false);
    }

    public void InstantiateWarrior(Vector3 position, Quaternion rotation)
    {
        if (CurrentWarriorsCount < WarriorsCountLimit)
        {
            Warrior warrior = Instantiate(_warriorPrefab, position, rotation);
            _warriors.Add(warrior);

            warrior.Initialize(this);

            UpdateWarriorsAmountUI();
        }
    }

    public void RemoveWarrior(Warrior warrior)
    {
        _warriors.Remove(warrior);
        UpdateWarriorsAmountUI();

        if (CurrentWarriorsCount <= 0)
        {
            CurrentGameState = GameState.Setup;
        }
    }

    private void UpdateWarriorsAmountUI()
    {
        _warriorsAmountText.text = CurrentWarriorsCount + "/" + WarriorsCountLimit;
        if (CurrentWarriorsCount > 1)
        {
            CurrentGameState = GameState.Game;
        }
    }

    public void InstantiateWarriorOnSimulate()
    {
        _position = new Vector3(Random.Range(-10, 10), 0, Random.Range(10, 20));
        InstantiateWarrior(_position, Quaternion.identity);
    }

    public void EnableSetupState()
    {
        CurrentGameState = GameState.Setup;
        _setupModeText.GetComponent<TextMeshProUGUI>().color = Color.cyan; 
    }

    public void EnableDestroyState()
    {
        CurrentGameState = GameState.Destroy;
    }

    public void Restart()
    {
        SceneManager.LoadScene("AR_DeathMatch");
        EnableGUI();
    }
}
