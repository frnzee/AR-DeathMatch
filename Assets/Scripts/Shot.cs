using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float _speed;

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
            if (_currentEnemy.IsDead)
            {
                _shooter._unitStats.HealUp();
                _shooter._unitStats.IncreaseDamage();
                _shooter._unitStats.IncreaseShootingSpeed();
            }
            else
            {
                _currentEnemy._unitStats.TakeDamage(_shooter._unitStats.Damage);
            }

            Destroy(gameObject);
        }
    }

    public void Initialize(Warrior shooter, Warrior currentEnemy)
    {
        _shooter = shooter;
        _currentEnemy = currentEnemy;
    }
}
