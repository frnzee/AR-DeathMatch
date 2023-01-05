using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private const float DefaultLerpSpeed = 3f;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _healthFiller;
    [SerializeField] private UnitStats _unitStats;
    
    private Camera _mainCamera;

    private float _lerpSpeed;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _lerpSpeed = DefaultLerpSpeed * Time.deltaTime;

        HealthBarFill();
        ChangeColor();

        _healthText.text = _unitStats.Health.ToString();
    }

    private void LateUpdate()
    {
        if (_mainCamera != null)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
    private void HealthBarFill()
    {
        _healthFiller.fillAmount = Mathf.Lerp(_healthFiller.fillAmount, (_unitStats.Health / _unitStats.MaxHealth), _lerpSpeed);
    }

    private void ChangeColor()
    {
        _healthFiller.color = Color.Lerp(Color.red, Color.green, (_unitStats.Health / _unitStats.MaxHealth));
    }
}