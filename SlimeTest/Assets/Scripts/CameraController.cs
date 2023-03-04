using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTarget;
    [SerializeField] private float _moveSpeed;

    private Vector3 _offset;


    private void Start()
    {
        _offset = _playerTarget.InverseTransformPoint(transform.position);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            _playerTarget.position + transform.rotation * _offset,
            _moveSpeed * Time.fixedDeltaTime);
    }
}
