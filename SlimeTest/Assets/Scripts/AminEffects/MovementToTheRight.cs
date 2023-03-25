using UnityEngine;

/// <summary>
/// 1) Передвижение объекта вправо
/// </summary>
public class MovementToTheRight : MonoBehaviour
{
    [SerializeField] private float _minValue = 0.7f;
    [SerializeField] private float _maxValue = 1f;
    
    private float _speed;

    private void Awake()
    {
        _speed = Random.Range(_minValue, _maxValue);
    }

    private void Update()
    {
        transform.Translate ( Vector3.right * _speed * Time.deltaTime);
    }
}
