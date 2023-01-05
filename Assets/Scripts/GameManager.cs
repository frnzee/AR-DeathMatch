using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public partial class GameManager : MonoBehaviour
{
    private const int WarriorsAmountLimit = 5;

    [SerializeField] private Warrior _warriorPrefab;
    [SerializeField] private GameObject _game_UI;
    [SerializeField] private GameObject _setupModeText;
    [SerializeField] private GameObject _destroyModeText;
    [SerializeField] private List<Warrior> _warriors;
    [SerializeField] private TextMeshProUGUI _warriorsAmountText;

    public IEnumerable<Warrior> Warriors => _warriors;
    public GameState CurrentGameState
    {
        get; private set;
    }

    private int _currentWarriorsAmount;
    private Vector3 _position;

    private void Start()
    {
        CurrentGameState = GameState.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnClick();
        }

        if (CurrentGameState == GameState.Game)
        {
            DisableGUI();
        }
        else if (CurrentGameState == GameState.None ||
                 CurrentGameState == GameState.Setup ||
                 CurrentGameState == GameState.Destroy)
        {
            EnableGUI();
        }
    }

    private void EnableGUI()
    {
        _game_UI.SetActive(true);
    }

    private void DisableGUI()
    {
        _game_UI.SetActive(false);
    }

    public void InstantiateWarrior(Vector3 position, Quaternion rotation)
    {
        if (_currentWarriorsAmount < WarriorsAmountLimit)
        {
            Warrior warrior = Instantiate(_warriorPrefab, position, rotation);
            _warriors.Add(warrior);

            warrior.LinkWithGameManager(this);

            UpdateWarriorsAmount();
        }
    }

    public void RemoveWarrior(Warrior warrior)
    {
        _warriors.Remove(warrior);
        UpdateWarriorsAmount();

        if (_currentWarriorsAmount == 1)
        {
            CurrentGameState = GameState.Setup;
        }
        else if (_currentWarriorsAmount <= 0)
        {
            CurrentGameState = GameState.None;
        }
    }

    private void UpdateWarriorsAmount()
    {
        _currentWarriorsAmount = _warriors.Count;
        _warriorsAmountText.text = _currentWarriorsAmount + "/" + WarriorsAmountLimit;
        if (_currentWarriorsAmount == WarriorsAmountLimit)
        {
            CurrentGameState = GameState.Game;
        }
    }

    public void OnClick()
    {
        _position = new Vector3(Random.Range(-10, 10), 0, Random.Range(5, 20));
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
