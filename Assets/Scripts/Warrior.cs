using System.Collections;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private const float MinRotationValue = -0.1f;
    private const float MaxRotationValue = 0.1f;
    private const float TimeForDeath = 5f;

    [SerializeField] private GameObject _appearingPrefab;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _healUpText;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _rotationSpeed;

    private GameManager _gameManager;
    private Warrior _currentEnemy;
    private Animator _warriorAnimator;
    private Quaternion _bulletQuaternion;

    private float _targetRotation;
    private float _startRotation;
    private float _shootingTimer;
    private float _deathExplosionTimer = TimeForDeath;

    private bool _isShooting = false;
    private bool _isSpawned = false;

    public UnitStats UnitStats { get; private set; }
    public bool IsDead { get; private set; }

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
        InitializeSubSystems();
    }

    private void InitializeSubSystems()
    {
        UnitStats = new UnitStats();
        _healthBar.Initialize(UnitStats);
    }

    private void Start()
    {
        Instantiate(_appearingPrefab, transform.position, transform.rotation);

        _warriorAnimator = GetComponent<Animator>();
        _isSpawned = true;
        IsDead = false;
    }

    private void Update()
    {
        if (FindClosestEnemy())
        {
            if (_isSpawned && _gameManager.CurrentGameState != GameManager.GameState.None)
            {
                RotateToNearestEnemy();
                Shooting();
            }
        }
        else
        {
            _warriorAnimator.SetFloat("TurnOrIdle", 0);
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
                }
            }
        }

        return _currentEnemy;
    }

    private void RotateToNearestEnemy()
    {
        Vector3 direction = _currentEnemy.transform.position - transform.position;
        _bulletQuaternion = Quaternion.LookRotation(direction);
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);

        if (_currentEnemy.IsDead)
        {
            _targetRotation = 0;
        }
        else _targetRotation = rotation.y;

        _warriorAnimator.SetFloat("TurnOrIdle", _targetRotation);

        _startRotation = _targetRotation;

        _isShooting = (Mathf.Abs(_startRotation) - Mathf.Abs(_targetRotation)) > MinRotationValue &&
                      (Mathf.Abs(_startRotation) - Mathf.Abs(_targetRotation)) < MaxRotationValue;
    }

    private void Shooting()
    {
        _shootingTimer -= Time.deltaTime;

        if (!_currentEnemy.IsDead && !IsDead && _isShooting && _shootingTimer <= 0)
        {
            _warriorAnimator.SetTrigger("Shoot");
            Bullet bullet = Instantiate(_bulletPrefab, transform.position, _bulletQuaternion);
            bullet.Initialize(this);
            _shootingTimer = UnitStats.ShootingSpeed;
        }
    }

    public void JumpOnHealUp()
    {
        _warriorAnimator.SetTrigger("Jump");
        StartCoroutine(ShowHealthUpMessage(3f));
    }

    private void Die()
    {
        if (UnitStats.Health <= 0)
        {
            IsDead = true;

            _warriorAnimator.SetTrigger("Die");
            _gameManager.RemoveWarrior(this);

            ExplodeAfterDeath();

            Destroy(gameObject, TimeForDeath);
        }
        else
        {
            IsDead = false;
        }
    }

    private void ExplodeAfterDeath()
    {
        _deathExplosionTimer -= Time.deltaTime;
        if (_deathExplosionTimer <= 0)
        {
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
        }
    }

    private IEnumerator ShowHealthUpMessage(float time)
    {
        _healUpText.SetActive(true);
        yield return new WaitForSeconds(time);
        _healUpText.SetActive(false);
    }
}
