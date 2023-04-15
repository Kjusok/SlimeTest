using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    [SerializeField] private PlayerAnimations _playerAnimations;
    
    public Wallet Wallet { get; private set; }
    public StatsController StatsController { get; private set; }
    public bool IsDead { get; private set; }
    
    public event Action<float> GotHit;
    public event Action Dead; 


    private void Awake()
    {       
        Wallet = new Wallet();
        StatsController = new StatsController();
    }

    private void Start()
    {
        StatsController.SpeedAttackChanged += OnSpeedAttackChanged;
    }

    private void OnSpeedAttackChanged(float speedAttack)
    {
        _playerAnimations.CreateSpeedAttack(speedAttack);
    }

    private void OnDestroy()
    {
        StatsController.SpeedAttackChanged -= OnSpeedAttackChanged;
    }

    public void TakeDamage(float damage)
    {
        StatsController.DecreaseHealth(damage);
        _playerAnimations.Hurt();

        GotHit?.Invoke(damage);
        
        if(StatsController.Health <= 0)
        {
            Dead?.Invoke();
            _playerAnimations.Death();
            IsDead = true;
        }
    }
}