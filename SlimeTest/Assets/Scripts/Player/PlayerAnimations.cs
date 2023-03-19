using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int _runStateName = Animator.StringToHash("Run");
    private readonly int _attackStateName = Animator.StringToHash("Attack");
    private readonly int _hurtStateName = Animator.StringToHash("Hurt");
    private readonly int _deathStateName = Animator.StringToHash("Death");
    private readonly int _attackSpeedStateName = Animator.StringToHash("AttackSpeed");

    
    public void CreateSpeedAttack(float speed)
    {
        _animator.SetFloat(_attackSpeedStateName, speed);
    }

    public void PlayRun(bool flag)
    {
        _animator.SetBool(_runStateName, flag);
    }

    public void Attack()
    {
        _animator.SetTrigger(_attackStateName);
    }

    public void Hurt()
    {
        _animator.SetTrigger(_hurtStateName);
    }

    public void Death()
    {
        _animator.SetTrigger(_deathStateName);
    }
}
