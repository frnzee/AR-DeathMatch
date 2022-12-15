using UnityEngine;

public class Warrior : MonoBehaviour
{
    private const int DefaultHealth = 50;
    private const int MaxHealth = 100;
    private const int MinHealth = 0;
    private const int DefaultDamage = 10;
    private const int MinDamage = 1;
    private const int MaxDamage = 33;
    private const float MinRotationValue = -0.1f;
    private const float MaxRotationValue = 0.1f;

    private Animator _ar_Robot;
    //public UnitStats _unitStats;

    /*public int Health
    {
        get; private set;
    }*/

    private void Awake()
    {
        //_unitStats = GetComponentInChildren<UnitStats>();
    }

    private void Start()
    {
        _ar_Robot = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ar_Robot.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _ar_Robot.SetTrigger("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _ar_Robot.SetTrigger("Die");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _ar_Robot.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
        }
    }
}
