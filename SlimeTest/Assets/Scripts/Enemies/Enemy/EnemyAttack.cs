using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private const float NexHitDelay = 1;
    
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _damage = 5;

    private float _timerToNextHit;

    
    private void Update()
    {
        if (_timerToNextHit >= 0)
        {
            _timerToNextHit -= Time.deltaTime;
        }

        if (_enemy.IsAttack && _timerToNextHit < 0 && !_enemy.Player.IsDead)
        {
            _enemy.Player.TakeDamage(_damage);
            _timerToNextHit = NexHitDelay;
        }
    }
}