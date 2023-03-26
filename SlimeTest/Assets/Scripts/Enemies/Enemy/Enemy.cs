using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
/// <summary>
/// 1) Обнавляет отображение жизнец
/// 2) Получать урон
/// </summary>

public class Enemy : MonoBehaviour, IDamageble
{
    private const int BaseScore = 100;

    [SerializeField] private Image _heathBar;
    [SerializeField] private EnemiesWaveController _enemiesWaveController;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private EnemiesAnimations _enemiesAnimations;
    [SerializeField] private GameObject _deathPrefab;
    [SerializeField] private float _maxHealth = 100;

    private int _score;
    private float _heath;


    public bool IsDead { get; private set; }
    public bool IsAttack{ get; private set; }

    public event Action<float> GotHit;

   
    public void Initialize(Wallet wallet, EnemiesWaveController enemiesWaveController)
    {
        _wallet = wallet;
        _enemiesWaveController = enemiesWaveController;
        _score = BaseScore;
        _heath = _maxHealth;
    }

   public void AttackState(bool isAttack)
    {
        IsAttack = isAttack;
    }
    
    public void TakeDamage(float damage)
    {
        _heath -= damage;
    
        _enemiesAnimations.Hurt();
    
        _heathBar.fillAmount = _heath / _maxHealth;
    
        GotHit?.Invoke(damage);
    
        if (_heath <= 0)
        {
            IsDead = true;
    
            _wallet.AddCoinsToWallet(_score);
            _enemiesWaveController.RemoveEnemyFromList(this);
            
            Destroy(gameObject);
    
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }
}