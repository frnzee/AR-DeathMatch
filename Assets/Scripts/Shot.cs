using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float _speed;

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
            _currentEnemy._unitStats.TakeDamage(5);
            Destroy(gameObject);
        }
    }

    public void InitializeEnemy(Warrior currentEnemy)
    {
        _currentEnemy = currentEnemy;
    }
}
