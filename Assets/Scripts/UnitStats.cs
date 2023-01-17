using UnityEngine;

public class UnitStats
{
    private const int DefaultHealth = 75;
    private const int MinHealthValue = 0;
    private const int MaxHealthValue = 100;
    private const int DefaultDamage = 15;
    private const int MaxDamageValue = 33;
    private const int RandomHealthChanger = 25;
    private const int RandomDamageChanger = 10;
    private const int DamageIncrementValue = 5;
    private const int HealthUpAmountValue = 25;

    private const float DefaultShootingSpeed = 3f;
    private const float MaxShootingSpeedValue = 0.5f;
    private const float ShootingSpeedIncrement = 0.2f;
    private const float RandomShootingSpeedChanger = 2f;

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

    public void Initialize()
    {
        _health = (Random.Range(-1, 1f) * RandomHealthChanger) + DefaultHealth;
        _damage = (Random.Range(-1, 1f) * RandomDamageChanger) + DefaultDamage;
        _shootingSpeed = Random.Range(-1, 1f) * RandomShootingSpeedChanger + DefaultShootingSpeed;
        DamageIncrement = DamageIncrementValue;
    }

    public void IncreaseDamage()
    {
        Damage += DamageIncrement;
    }

    public void IncreaseShootingSpeed()
    {
        ShootingSpeed -= ShootingSpeedIncrement;
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
