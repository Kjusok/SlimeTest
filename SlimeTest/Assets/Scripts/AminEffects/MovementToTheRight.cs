using UnityEngine;

public class MovementToTheRight : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 0.7f;
    [SerializeField] private float _maxSpeed = 1f;
    
    private float _speed;

    
    private void Awake()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
