using System.Collections;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private const float DefaultMessageLifetime = 3f;

    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _setupModeText;
    [SerializeField] private GameObject _setupModeButton;
    [SerializeField] private GameObject _warriorsAmount;
    [SerializeField] private GameObject _setupUnitButton;
    [SerializeField] private TextMeshProUGUI _warriorsCountText;
    [SerializeField] private TextMeshProUGUI _messageText;

    private GameManager _gameManager;
    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
        _gameManager.GameStateChanged += OnGameStateChange;
        _gameManager.WarriorsCountChanged += OnWarriorsCountChange;
    }

    private void OnWarriorsCountChange(int count)
    {
        UpdateWarriorsCount(count);
    }

    private void OnGameStateChange(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.None:
                EnableGUI();
                break;
            case GameManager.GameState.Game:
                DisableGUI();
                break;
            case GameManager.GameState.Setup:
                EnableGUI();
                _setupModeText.GetComponent<TextMeshProUGUI>().color = Color.cyan;
                StartCoroutine(ShowMessage("Setup mode", DefaultMessageLifetime));
                break;
        }
    }

    private void EnableGUI()
    {
        _setupModeButton.SetActive(true);
        _restartButton.SetActive(true);
        _setupUnitButton.SetActive(true);
        _warriorsAmount.SetActive(true);
    }

    private void DisableGUI()
    {
        _setupUnitButton.SetActive(false);
        _setupModeButton.SetActive(false);
    }

    private void UpdateWarriorsCount(int count)
    {
        _warriorsCountText.text = count + "/" + _gameManager.WarriorsLimit;
    }

    private IEnumerator ShowMessage(string text, float time)
    {
        _messageText.text = text;
        yield return new WaitForSeconds(time);
        _messageText.text = null;
        _setupModeText.GetComponent<TextMeshProUGUI>().color = Color.black;
    }

    private void OnDestroy()
    {
        _gameManager.GameStateChanged -= OnGameStateChange;
    }
}
