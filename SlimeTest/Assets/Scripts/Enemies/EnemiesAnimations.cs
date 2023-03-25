using System;
using UnityEngine;

/// <summary>
///  Адаптирует анимации
/// </summary>
[RequireComponent(typeof(Animator))]
public class EnemiesAnimations : MonoBehaviour
{
    private readonly int _walkStateName = Animator.StringToHash("Walk");
    private readonly int _hurtStateName = Animator.StringToHash("Hurt");
    private readonly int _attackStateName = Animator.StringToHash("Attack");

    private Animator _animator;

    
    protected void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
