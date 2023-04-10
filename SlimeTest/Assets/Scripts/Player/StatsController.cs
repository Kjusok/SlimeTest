using System;

/// 1) Изменяет характеристики игрока (Жизни, Скорость атаки, Общее здоровье, востановление здоровья, получение урона)
public class StatsController
{    
    private const int StartDamage = 15;
    private const int StartHealth = 100;
    private const float AttackSpeedStep = 0.1f;
    private const int RecoveryHealthValue = 5;
    private const int ValueForHealthUp = 10;

    private float _maxHealth;
    private float _startSpeedAttack = 2;
    
    public int Damage { get; private set; }
    public float Health { get; private set; }
    
    public event Action<int> DamageChanged;
    public event Action<float,float> HealthChanged;
    public event Action<float> SpeedAttackChanged;
    public event Action<float> MaxHealthChanged;

    public StatsController()
    {
        Damage = StartDamage;
        _maxHealth = StartHealth;
        Health = _maxHealth;
    }
    
    public void DecreaseHealth(float damage)
    {
        Health -= damage;
        HealthChanged?.Invoke(Health,_maxHealth);
       // _heathBar.fillAmount = Health / _maxHealth;
    }
    
    public void HealthRecovery()
    {
        if (Health < _maxHealth)
        {
            Health += RecoveryHealthValue;
            HealthChanged?.Invoke(Health, _maxHealth);
            //_heathBar.fillAmount = Health / _maxHealth;
        }
    }

    public void UpHealth()
    {
        _maxHealth += ValueForHealthUp;
        MaxHealthChanged?.Invoke(_maxHealth);
       //_currentHealthUpText.text = _maxHealth.ToString();
    }

    public void UpDamage()
    {
        Damage++;
        DamageChanged?.Invoke(Damage);
      // _currentDamageText.text = Damage.ToString();
    }

    public void UpSpeedAttack()
    {
        _startSpeedAttack += AttackSpeedStep;
        SpeedAttackChanged?.Invoke(_startSpeedAttack);

      // _playerAnimations.CreateSpeedAttack(_startSpeedAttack);
      // _currentAttackSpeedText.text = _startSpeedAttack.ToString("0.00");
    }
}