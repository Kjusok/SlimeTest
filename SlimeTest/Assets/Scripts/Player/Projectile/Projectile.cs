using UnityEngine;

/// <summary>
/// 1) Перемещение снаряда по дуге
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] private int _speed;

    private Transform _target;
    private Vector3 _currentPosition;
    private Vector3 _origin;
    private float _distanceTravelled;
    private float _arcFactor = 0.8f;

    public float Damage { get; private set; }

    
    public void Launch(Transform target, float damage)
    {
        _target = target;
        Damage = damage;
        
        _origin = transform.position;
        _currentPosition = transform.position;
    }
    
    private void Update()
    {
        if (!_target)
        {
            Debug.LogError("Target not specified");
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
}