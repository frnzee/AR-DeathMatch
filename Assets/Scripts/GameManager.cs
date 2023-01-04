using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private const int WarriorsAmountLimit = 5;

    [SerializeField] private Warrior _warriorPrefab;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private GameObject _setupModeText;
    [SerializeField] private GameObject _destroyModeText;
    [SerializeField] private List<Warrior> _warriors;
    [SerializeField] private TextMeshProUGUI _warriorsAmountText;

    public IEnumerable<Warrior> Warriors => _warriors;

    private int _currentWarriorsAmount;
    private Vector3 _position;

    public bool IsSetupState
    {
        get; private set;
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnClick();
        }
    }

    private void AddNumbersOnUI()
    {
        _warriorsAmountText.text = _currentWarriorsAmount + "/" + WarriorsAmountLimit;
    }

    public void InstantiateWarrior(Vector3 position, Quaternion rotation)
    {
        if (_currentWarriorsAmount < WarriorsAmountLimit)
        {
            Warrior warrior = Instantiate(_warriorPrefab, position, rotation);
            _warriors.Add(warrior);

            warrior.Initialize(this);

            _currentWarriorsAmount = _warriors.Count;
            AddNumbersOnUI();
        }
    }

    public void RemoveWarrior(Warrior warrior)
    {
        _warriors.Remove(warrior);
        _currentWarriorsAmount = _warriors.Count;
        _warriorsAmountText.text = _currentWarriorsAmount + "/" + WarriorsAmountLimit;
    }
    public void OnClick()
    {
        _position = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
        InstantiateWarrior(_position, Quaternion.identity);
    }
}
