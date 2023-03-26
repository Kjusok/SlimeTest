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
    [SerializeField] private StatsController _statsController;
    
    public bool IsDead { get; private set; }
    
    public event Action<float> GotHit;


    public void TakeDamage(float damage)
    {
        _statsController.DecreaseHealth(damage);
        _playerAnimations.Hurt();

        GotHit?.Invoke(damage);
        
        if(_statsController.Health <= 0)
        {
            _gameStatesController.AwakePanelYouDied();
            _playerAnimations.Death();
            IsDead = true;
        }
    }
}