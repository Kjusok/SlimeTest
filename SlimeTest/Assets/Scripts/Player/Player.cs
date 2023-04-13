using System;
using UnityEngine;
/// <summary>
/// 1) Получать урон
/// 2) Хранит состояние умер или нет
/// </summary>
public class Player : MonoBehaviour, IDamageble
{
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private GameStatesController _gameStatesController;
    
    public Wallet Wallet { get; private set; }
    public StatsController StatsController { get; private set; }
    public bool IsDead { get; private set; }
    
    public event Action<float> GotHit;
    
    
    private void Awake()
    {       
        Wallet = new Wallet();

        StatsController = new StatsController();
    }

    public void TakeDamage(float damage)
    {
        StatsController.DecreaseHealth(damage);
        _playerAnimations.Hurt();

        GotHit?.Invoke(damage);
        
        if(StatsController.Health <= 0)
        {
            _gameStatesController.PlayerDead();
            _playerAnimations.Death();
            IsDead = true;
        }
    }
}