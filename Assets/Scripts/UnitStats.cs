using UnityEngine;

public class UnitStats : MonoBehaviour
{
    private const int DefaultHealth = 50;
    private const int RandomChanger = 10;
    private const int MaxHealthValue = 100;
    private const int MinHealthValue = 0;
    private const int DefaultDamage = 15;
    private const int MaxDamageValue = 33;
    private const float DefaultShootingSpeed = 3f;
    private const float MaxShootingSpeedValue = 0.5f;
    private const float ShootingSpeedIncrement = 0.2f;
    private const int DamageIncrementValue = 5;
    private const int HealthUpAmountValue = 50;

    public float Health
    {
        get
        {
            if (_health > MaxHealth)
            {
                return MaxHealth;
            }
            else if (_health < MinHealthValue)
            {
                return MinHealthValue;
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
        _health = Random.Range(DefaultHealth - RandomChanger, DefaultHealth + RandomChanger);
        _damage = Random.Range(DefaultDamage - RandomChanger, DefaultDamage + RandomChanger);
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
