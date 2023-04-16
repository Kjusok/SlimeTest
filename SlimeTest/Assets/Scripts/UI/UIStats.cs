using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    [SerializeField] private Text _currentDamageText;
    [SerializeField] private Text _currentAttackSpeedText;
    [SerializeField] private Text _currentHealthUpText;
    [SerializeField] private Image _heathBar;
    [SerializeField] private Player _player;

    
    private void Start()
    {
        _player.StatsController.DamageChanged += OnDamageChanged;
        _player.StatsController.SpeedAttackChanged += OnSpeedAttackChanged;
        _player.StatsController.HealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        _player.StatsController.DamageChanged -= OnDamageChanged;
        _player.StatsController.SpeedAttackChanged -= OnSpeedAttackChanged;
        _player.StatsController.HealthChanged -= OnHealthChanged;
    }
    
    private void OnDamageChanged(int damage)
    {
        _currentDamageText.text = damage.ToString();
    }

    private void OnHealthChanged(float health, float maxHealth)
    {
        _heathBar.fillAmount = health / maxHealth;
        _currentHealthUpText.text = maxHealth.ToString();
    }

    private void OnSpeedAttackChanged(float speedAttack)
    {
        _currentAttackSpeedText.text = speedAttack.ToString("0.00");
    }
}