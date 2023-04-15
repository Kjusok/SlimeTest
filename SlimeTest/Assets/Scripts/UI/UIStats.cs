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
        _player.StatsController.DamageChanged += DamageChangedHandler;
        _player.StatsController.SpeedAttackChanged += SpeedAttackChangedHandler;
        _player.StatsController.HealthChanged += HealthChangedHandler;
    }

    private void OnDestroy()
    {
        _player.StatsController.DamageChanged -= DamageChangedHandler;
        _player.StatsController.SpeedAttackChanged -= SpeedAttackChangedHandler;
        _player.StatsController.HealthChanged -= HealthChangedHandler;
    }
    
    private void DamageChangedHandler(int damage)
    {
        _currentDamageText.text = damage.ToString();
    }

    private void HealthChangedHandler(float health, float maxHealth)
    {
        _heathBar.fillAmount = health / maxHealth;
        _currentHealthUpText.text = maxHealth.ToString();
    }

    private void SpeedAttackChangedHandler(float speedAttack)
    {
        _currentAttackSpeedText.text = speedAttack.ToString("0.00");
    }
}