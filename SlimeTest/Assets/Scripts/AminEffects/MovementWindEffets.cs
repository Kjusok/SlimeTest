using UnityEngine;

public class MovementWindEffets : MonoBehaviour
{
    private float _speed;

    private void Awake()
    {
        _speed = Random.Range(0.7f, 1f);
    }

    private void Update()
    {
        transform.Translate ( Vector3.right * _speed * Time.deltaTime);
    }
}
