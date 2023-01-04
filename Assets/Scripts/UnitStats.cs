using UnityEngine;

public class UnitStats : MonoBehaviour
{
    private const float DefaultHealth = 50f;
    private const float MaxHealthValue = 100f;
    private const float DefaultDamage = 10f;
    private const float MaxDamageValue = 33f;
    private const float DefaultShootingSpeed = 3f;
    private const float MaxShootingSpeedValue = 0.5f;
    private const float ShootingSpeedIncrement = 0.2f;
    private const float DamageIncrementValue = 5f;
    private const float HealthUpAmountValue = 50f;

    public float Health
    {
        get
        {
            if (_health > MaxHealth)
            {
                return MaxHealth;
            }
            else
            {
                return _health;
            }
        }
        private set
        {
            _health = value;
        }
    }
    public float MaxHealth
    {
        get
        {
            return MaxHealthValue;
        }
        private set
        {

        }
    }
    public float HealthUpAmount
    {
        get
        {
            return HealthUpAmountValue;
        }
        private set
        {

        }
    }
    public float Damage
    {
        get
        {
            if (_damage >= MaxDamage)
            {
                return MaxDamage;
            }
            else
            {
                return _damage;
            }
        }
        private set
        {
            _damage = value;
        }
    }
    public float MaxDamage
    {
        get
        {
            return MaxDamageValue;
        }
        private set
        {

        }
    }
    public float DamageIncreament
    {
        get; private set;
    }
    public float ShootingSpeed
    {
        get
        {
            if (_shootingSpeed <= MaxShootingSpeed)
            {
                return MaxShootingSpeedValue;
            }
            else
            {
                return _shootingSpeed;
            }
        }
        private set
        {
            _shootingSpeed = value;
        }
    }
    public float MaxShootingSpeed
    {
        get
        {
            return MaxShootingSpeedValue;
        }
    }

    private float _health;
    private float _damage;
    private float _shootingSpeed;

    private void Start()
    {
        _health = DefaultHealth;
        _damage = DefaultDamage;
        DamageIncreament = DamageIncrementValue;
        _shootingSpeed = DefaultShootingSpeed;
    }

    public void IncreaseDamage()
    {
        Damage += DamageIncreament;
    }

    public void IncreaseShootingSpeed()
    {
        ShootingSpeed -= ShootingSpeedIncrement;
    }

    public void TakeDamage(float DamageAmount)
    {
        Health -= DamageAmount;
    }

    public void HealUp()
    {
        Health += HealthUpAmount;
    }
}
