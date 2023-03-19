using UnityEngine;
/// <summary>
/// 1) Перемещение снаряда по дуге
/// 2) Нанисение урона снарядом
/// 3) Косание со врагом
/// 4) Создание эффекта взырва снаряда
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private GameObject _explodionPrefab;

    private Transform _target;
    private Vector3 _currentPosition;
    private Vector3 _origin;
    private float _distanceTravelled;
    private float _arcFactor = 0.8f;

    private int _damage;


    private void OnEnable()
    {
        _origin = transform.position;
        _currentPosition = transform.position;
    }

    private void Update()
    {
        if (!_target)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = _target.position - _currentPosition;
        _currentPosition += direction.normalized * _speed * Time.deltaTime;
        _distanceTravelled += _speed * Time.deltaTime;

        float totalDistance = Vector3.Distance(_origin, _target.position);
        float heightOffset = _arcFactor * totalDistance * Mathf.Sin(_distanceTravelled * Mathf.PI / totalDistance);
        transform.position = _currentPosition + new Vector3(0, 0, heightOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.TakeDamage(_damage);

            Destroy(gameObject);
            Instantiate(_explodionPrefab, transform.position, Quaternion.identity);
        }
    }

    public void Initialize(Transform target, int damage)
    {
        _target = target;
        _damage = damage;
    }
}
