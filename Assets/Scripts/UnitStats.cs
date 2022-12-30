using System;
using UnityEngine;

[Serializable]

public class UnitStats : MonoBehaviour
{
    private const float DefaultHealth = 50f;
    private const float MaxHealthValue = 100f;
    private const float DefaultDamage = 10f;
    private const float MaxDamageValue = 33f;
    private const float DefaultShootingSpeed = 3f;
    private const float MaxShootingSpeedValue = 10f;

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
    public float ShootingSpeed
    {
        get
        {
            if (_shootingSpeed >= MaxShootingSpeed)
            {
                return MaxDamage;
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
        _shootingSpeed = DefaultShootingSpeed;
    }

    private void Update()
    {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        else if (Health < 0)
        {
            Health = 0;
        }

        if (Damage > MaxDamage)
        {
            Damage = MaxDamage;
        }
    }

    public void IncreaseDamage(int IncreaseDamageAmount)
    {
        Damage += IncreaseDamageAmount;
    }

    public void TakeDamage(float DamageAmount)
    {
        Health -= DamageAmount;
    }

    public void HealUp(float HealUpAmount)
    {
        Health += HealUpAmount;
    }
}
