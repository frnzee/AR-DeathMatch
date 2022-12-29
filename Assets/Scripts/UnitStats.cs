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
    private const float MinDamage = 1f;

    public float Health
    {
        get; private set;
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
        get; private set;
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
        get; private set;
    }
    public float MaxShootingSpeed
    {
        get
        {
            return MaxShootingSpeedValue;
        }
    }

    private void Start()
    {
        Health = DefaultHealth;
        Damage = DefaultDamage;
        ShootingSpeed = DefaultShootingSpeed;
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
        else if (Damage < MinDamage)
        {
            Damage = MinDamage;
        }
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
