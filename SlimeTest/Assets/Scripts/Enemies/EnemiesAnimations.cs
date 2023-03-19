using UnityEngine;

/// <summary>
/// Воспроизведение анимаций
/// </summary>
public class EnemiesAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int _walkStateName = Animator.StringToHash("Walk");
    private readonly int _hurtStateName = Animator.StringToHash("Hurt");
    private readonly int _attackStateName = Animator.StringToHash("Attack");


    public void Walk(bool flag)
    {
        _animator.SetBool(_walkStateName, flag);
    }

    public void Attack(bool flag)
    {
        _animator.SetBool(_attackStateName, flag);
    }

    public void Hurt()
    {
        _animator.SetTrigger(_hurtStateName);
    }
}
