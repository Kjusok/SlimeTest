using UnityEngine;
using UnityEngine.UI;
/// 1) Изменяет характеристики игрока (Жизни, Скорость атаки, Общее здоровье, востановление здоровья, получение урона)
public class StatsController : MonoBehaviour
{    
    private const int StartDamage = 15;
    private const int StartHealth = 100;
    private const float AttackSpeedStep = 0.1f;
    private const int RecoveryHealthValue = 5;
    private const int ValueForHealthUp = 10;
    
    [SerializeField] private Text _currentDamageText;
    [SerializeField] private Text _currentAttackSpeedText;
    [SerializeField] private Text _currentHealthUpText;
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private Image _heathBar;

    private float _maxHealth;
    private float _startSpeedAttack = 2;
    
    public int Damage { get; private set; }
    public float Health { get; private set; }

    private void Awake()
    {
        Damage = StartDamage;
        _maxHealth = StartHealth;
        Health = _maxHealth;
    }

    public void DecreaseHealth(float damage)
    {
        Health -= damage;
        _heathBar.fillAmount = Health / _maxHealth;
    }
    
    public void HealthRecovery()
    {
        if (Health < _maxHealth)
        {
            Health += RecoveryHealthValue;
            _heathBar.fillAmount = Health / _maxHealth;
        }
    }

    public void UpHealth()
    {
        _maxHealth += ValueForHealthUp;
        _currentHealthUpText.text = _maxHealth.ToString();
    }

    public void UpDamage()
    {
        Damage++;
        _currentDamageText.text = Damage.ToString();
    }

    public void UpSpeedAttack()
    {
        _startSpeedAttack += AttackSpeedStep;

        _playerAnimations.CreateSpeedAttack(_startSpeedAttack);
        _currentAttackSpeedText.text = _startSpeedAttack.ToString("0.00");
    }
}