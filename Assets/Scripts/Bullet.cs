using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _shotExplosionPrefab;

    private Warrior _shooter;

    private float _lifeTime = 15f;

    public void Initialize(Warrior shooter) => _shooter = shooter;

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
        var currentEnemy = other.gameObject.GetComponent<Warrior>();
        if (currentEnemy && currentEnemy != _shooter)
        {
            currentEnemy.UnitStats.TakeDamage((int)_shooter.UnitStats.Damage);

            if (currentEnemy.IsDead && !_shooter.IsDead)
            {
                _shooter.UnitStats.IncreaseDamage();
                _shooter.UnitStats.IncreaseShootingSpeed();
            }

            Instantiate(_shotExplosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
