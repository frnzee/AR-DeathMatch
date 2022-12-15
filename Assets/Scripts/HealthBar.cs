using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _healthFiller;

    private UnitStats _unitStats;

    private float _lerpSpeed;

    void Start()
    {
        _unitStats = GetComponent<UnitStats>();
    }

    void Update()
    {
        _lerpSpeed = 3f * Time.deltaTime;

        HealthBarFill();
        ColorChanger();

        _healthText.text = _unitStats.Health.ToString();

        if (Input.GetKeyDown(KeyCode.S))
        {
            _unitStats.TakeDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _unitStats.HealUp(5);
        }
    }

    private void HealthBarFill()
    {
        _healthFiller.fillAmount = Mathf.Lerp(_healthFiller.fillAmount, (_unitStats.Health / _unitStats.MaxHealth), _lerpSpeed);
    }

    private void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (_unitStats.Health / _unitStats.MaxHealth));
        _healthFiller.color = healthColor;
    }
}