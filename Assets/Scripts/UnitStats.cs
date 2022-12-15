using System;
using UnityEngine;

[Serializable]

public class UnitStats : MonoBehaviour
{
    private const float DefaultHealth = 50f;
    private const float MinHealth = 0f;
    private const float DefaultDamage = 10f;
    private const float MinDamage = 1f;
    private const float MinRotationSpeed = -0.1f;
    private const float MaxRotationSpeed = 0.1f;

    public float Health = 50f;
    public float MaxHealth
    {
        get; private set;
    }

    public float Damage;
    public float MaxDamage
    {
        get; private set;
    }

    private void Awake()
    {
    }

    private void Start()
    {
        MaxHealth = 100f;
        MaxDamage = 33f;
        Health = DefaultHealth;
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
