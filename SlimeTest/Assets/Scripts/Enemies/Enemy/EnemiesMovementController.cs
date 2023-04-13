using UnityEngine;
/// 3) Перемещаться к цели (движение и вращение)

public class EnemiesMovementController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private EnemiesAnimations _enemiesAnimations;
    [SerializeField] private Enemy _enemy;
    
    private void Update()
    {
        RotationToTarget();
        MovementToTarget();
    }

    private void RotationToTarget()
    {
        if(_enemy.IsDead || _enemy.IsAttack)
        {
            return;
        }

        Vector3 direction = _enemy.Player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }

    private void MovementToTarget()
    {
        if (_enemy.IsAttack)
        {
            _enemiesAnimations.Walk(false);
            return;
        }

        _enemiesAnimations.Walk(true);
        transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
    }
}