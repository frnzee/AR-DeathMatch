using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _shotExplosionPrefab;

    private Warrior _shooter;
    private Warrior _currentEnemy;

    private float _lifeTime = 10f;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        transform.position += _speed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Warrior>() == _currentEnemy)
        {
            _currentEnemy.UnitStats.TakeDamage(_shooter.UnitStats.Damage);

            if (_currentEnemy.IsDead && !_shooter.IsDead)
            {
                _shooter.UnitStats.HealUp();
                _shooter.UnitStats.IncreaseDamage();
                _shooter.UnitStats.IncreaseShootingSpeed();
                _shooter.JumpOnHealUp();
            }

            Instantiate(_shotExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void Initialize(Warrior shooter, Warrior currentEnemy)
    {
        _shooter = shooter;
        _currentEnemy = currentEnemy;
    }
}
