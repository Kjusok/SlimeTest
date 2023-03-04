using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _speed;

    private Transform _targetPosition;
    private Vector3 _currentPosition;
    private Vector3 _origin;
    private float _distanceTravelled;
    private float _arcFactor = 0.8f;

    public int Damage;


    private void OnEnable()
    {
        _origin = _currentPosition = transform.position;
    }

    private void Update()
    {
        if (!_targetPosition)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = _targetPosition.position - _currentPosition;
        _currentPosition += direction.normalized * _speed * Time.deltaTime;
        _distanceTravelled += _speed * Time.deltaTime;

        float totalDistance = Vector3.Distance(_origin, _targetPosition.position);
        float heightOffset = _arcFactor * totalDistance * Mathf.Sin(_distanceTravelled * Mathf.PI / totalDistance);
        transform.position = _currentPosition + new Vector3(0, 0, heightOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.TakeDamage(Damage);

            Destroy(gameObject);
        }
    }

    public void GetTarget(Transform target)
    {
        _targetPosition = target;
    }
}
