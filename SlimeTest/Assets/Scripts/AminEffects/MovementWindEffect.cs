using UnityEngine;
/// <summary>
/// 1) Передвижение объекта вправо
/// </summary>
public class MovementWindEffect : MonoBehaviour
{
    private const float MinValue = 0.7f;
    private const float MaxValue = 1f;
    
    private float _speed;

    private void Awake()
    {
        _speed = Random.Range(MinValue, MaxValue);
    }

    private void Update()
    {
        transform.Translate ( Vector3.right * _speed * Time.deltaTime);
    }
}
