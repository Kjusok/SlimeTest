using UnityEngine;
/// 2) Способность атаковать

public class EnemyAttack : MonoBehaviour
{
    private const float NexHitDelay = 1;
    
    [SerializeField] private Player _target;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _damage = 5;

    private float _timerToNextHit;

    
    public void Initialize(Player target)
    {
        _target = target;
    }
    
    private void Update()
    {
        if (_timerToNextHit >= 0)
        {
            _timerToNextHit -= Time.deltaTime;
        }

        if (_enemy.IsAttack && _timerToNextHit < 0 && !_target.IsDead)
        {
            _target.TakeDamage(_damage);
            _timerToNextHit = NexHitDelay;
        }
    }
}