using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageble
{
    private const int BaseScore = 100;

    [SerializeField] private Image _heathBar;
    [SerializeField] private EnemiesAnimations _enemiesAnimations;
    [SerializeField] private GameObject _deathPrefab;
    [SerializeField] private float _maxHealth = 100;

    private float _heath;

    public Player Player { get; private set; }
    public int Score { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsAttack{ get; set; }

    public event Action<float> GotHit;
    public event Action<Enemy> Dead; 


    private void Awake()
    {
        Score = BaseScore;
        _heath = _maxHealth;
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
            
            Dead?.Invoke(this);
    
            Destroy(gameObject);
    
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }

    public void Initialize(Player player)
    {
        Player = player;
    }
}