using UnityEngine;

public class UnitStats : MonoBehaviour
{
    private const int DefaultHealth = 50;
    private const int MinHealthValue = 0;
    private const int MaxHealthValue = 100;
    private const int DefaultDamage = 15;
    private const int MaxDamageValue = 33;
    private const int RandomHealthChanger = 10;
    private const int RandomDamageChanger = 5;
    private const int DamageIncrementValue = 5;
    private const int HealthUpAmountValue = 50;

    private const float DefaultShootingSpeed = 3f;
    private const float MaxShootingSpeedValue = 0.5f;
    private const float ShootingSpeedIncrement = 0.2f;

    private float _health;
    private float _damage;
    private float _shootingSpeed;

    public float Health
    {
        get => Mathf.Clamp(_health, MinHealthValue, MaxHealthValue);
        private set => _health = value;
    }
    public float MaxHealth => MaxHealthValue;
    public int HealthUpAmount => HealthUpAmountValue;
    public float Damage
    {
        get => Mathf.Clamp(_damage, 0, MaxDamage);
        private set => _damage = value;
    }
    public int MaxDamage => MaxDamageValue;
    public int DamageIncrement
    {
        get; private set;
    }
    public float ShootingSpeed
    {
        get => _shootingSpeed;
        private set => _shootingSpeed = Mathf.Clamp(value, MaxShootingSpeed, DefaultShootingSpeed);
    }
    public float MaxShootingSpeed => MaxShootingSpeedValue;

    private void Start()
    {
        _health = (Random.Range(-1f, 1f) * RandomHealthChanger) + DefaultHealth;
        _damage = (Random.Range(-1f, 1f) * RandomDamageChanger) + DefaultDamage;
        DamageIncrement = DamageIncrementValue;
        _shootingSpeed = DefaultShootingSpeed;
    }

    public void IncreaseDamage()
    {
        Damage += DamageIncrement;
        Debug.Log("Damage: " + Damage);
    }

    public void IncreaseShootingSpeed()
    {
        ShootingSpeed -= ShootingSpeedIncrement;
        Debug.Log("Shooting speed: " + ShootingSpeed);
    }

    public void TakeDamage(int DamageAmount)
    {
        Health -= DamageAmount;
    }

    public void HealUp()
    {
        Health += HealthUpAmount;
    }
}
