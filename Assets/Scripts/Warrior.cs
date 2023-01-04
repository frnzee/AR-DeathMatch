using UnityEngine;

public class Warrior : MonoBehaviour
{
    private const float MinRotationValue = -0.1f;
    private const float MaxRotationValue = 0.1f;
    private const float TimeForDeath = 5;
    private const float TimeForIdleEventAnim = 10;

    [SerializeField] private Warrior _currentEnemy;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Shot _shotPrefab;
    [SerializeField] private int _speed;

    private GameManager _gameManager;
    private float _targetRotation;
    private float _startRotation;
    private float _shootingTimer;

    private bool _isShooting = false;
    private bool _isSpawned = false;

    public bool IsDead
    {
        get; private set;
    }

    private float _timerForIdleEventAnim = TimeForIdleEventAnim;

    private Animator _warriorAnimator;

    public UnitStats _unitStats
    {
        get; private set;
    }

    public int Health
    {
        get; private set;
    }

    private void Start()
    {
        _unitStats = GetComponentInChildren<UnitStats>();
        _warriorAnimator = GetComponent<Animator>();
        _isSpawned = true;
        IsDead = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _warriorAnimator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _warriorAnimator.SetTrigger("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _warriorAnimator.SetTrigger("Die");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _warriorAnimator.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }

        if (FindClosestEnemy())
        {
            if (_isSpawned)
            {
                RotateToNearestEnemy();
                Shooting();
            }
        }
        else
        {
            _timerForIdleEventAnim -= Time.deltaTime;

            if (_timerForIdleEventAnim <= 0)
            {
                _warriorAnimator.SetTrigger("Idle");
                _timerForIdleEventAnim = TimeForIdleEventAnim;
            }
        }
        Die();
    }

    private Warrior FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (Warrior warrior in _gameManager.Warriors)
        {
            if (position != warrior.transform.position)
            {
                Vector3 diff = warrior.transform.position - position;
                float currentDistance = diff.sqrMagnitude;
                if (currentDistance < distance)
                {
                    _currentEnemy = warrior;
                    distance = currentDistance;
                    UnitStats _currentEnemyUnitStats = _currentEnemy._unitStats;
                }
            }
        }

        return _currentEnemy;
        
    }

    private void RotateToNearestEnemy()
    {
        Vector3 direction = _currentEnemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speed * Time.deltaTime);

        _targetRotation = (float)Mathf.Round(rotation.y);

        if (!IsDead && _startRotation < _targetRotation)
        {
            _warriorAnimator.SetTrigger("TurnRight");
            _startRotation = (float)Mathf.Round(transform.rotation.y);
        }
        else if (!IsDead)
        {
            _warriorAnimator.SetTrigger("TurnLeft");
            _startRotation = (float)Mathf.Round(transform.rotation.y);
        }

        if ((Mathf.Abs(_startRotation) - Mathf.Abs(_targetRotation)) > MinRotationValue &&
            (Mathf.Abs(_startRotation) - Mathf.Abs(_targetRotation)) < MaxRotationValue)
        {
            _isShooting = true;
        }
    }

    private void Shooting()
    {
        _shootingTimer -= Time.deltaTime;

        if (!IsDead && _isShooting && _shootingTimer <= 0)
        {
            _warriorAnimator.SetTrigger("Shoot");
            Shot shot = Instantiate(_shotPrefab, transform.position, transform.rotation);
            shot.Initialize(this, _currentEnemy);
            _shootingTimer = _unitStats.ShootingSpeed;
        }
    }

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Die()
    {
        if (_unitStats.Health <= 0)
        {
            IsDead = true;
            _warriorAnimator.SetTrigger("Die");
            _gameManager.RemoveWarrior(this);
            Destroy(gameObject, TimeForDeath);
        }
        else
        {
            IsDead = false;
        }
    }
}
