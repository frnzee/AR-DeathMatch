using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private const float DefaultLerpSpeed = 2f;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _healthFiller;
    [SerializeField] private UnitStats _unitStats;
    [SerializeField] private GameObject _healthBar;

    private float _lerpSpeed;
    private Camera _mainCamera;

    public void Initialize(UnitStats unitStats)
    {
        _unitStats = unitStats;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _lerpSpeed = DefaultLerpSpeed * Time.deltaTime;

        HealthBarFill();
        ChangeColor();

        if (_unitStats.Health <= 0)
        {
            _healthText.text = "DEAD";
        }
        else
        {
            _healthText.text = Mathf.RoundToInt(_unitStats.Health).ToString();
        }

        _healthBar.SetActive(_unitStats.Health < _unitStats.MaxHealth);
    }

    private void LateUpdate()
    {
        if (_mainCamera != null)
        {
            transform.forward = _mainCamera.transform.forward;
        }
    }

    private void HealthBarFill()
    {
        _healthFiller.fillAmount = Mathf.Lerp(_healthFiller.fillAmount, _unitStats.Health / _unitStats.MaxHealth, _lerpSpeed);
    }

    private void ChangeColor()
    {
        _healthFiller.color = Color.Lerp(Color.red, Color.green, _unitStats.Health / _unitStats.MaxHealth);
    }
}