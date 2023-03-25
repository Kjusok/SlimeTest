using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// 1) Косаться трригера
/// 2) Находить близжайшего врага
/// 3) Перемещаться
/// 4) Спавниь эффект пыли из под ног
/// 5) Спавнить эффект быстрого перемещения (Полосы на экране)
/// 6) Показывать спылвающий текст над собой
/// 7) Атаковать (Включать анимацию атаки и создавать снаряд)
/// 8) Получать урон
/// 9) Востанавливать здоровье (Лечиться)
/// 10) Увеличивать запас жизней
/// 11) Увеличивать урон
/// 12) увеличивать скорость атаки
/// </summary>

public class Player : MonoBehaviour, IDamageble
{
    private const float MovementTime = 2.5f;
    private const float SpawnFlashEffectTime = 0.3f;
    private const float SpawnDustEffectTime = 1f;
    private const float TimeWhenNeedToSlowDown = 0.7f;
    private const float MovementOffsetRight = 0.9f;
    private const float MovementOffsetLeft = -0.8f;
    private const float AttackSpeedStep = 0.1f;
    private const int StartDamage = 15;
    private const int StartHealth = 100;
    private const int RecoveryHealthValue = 5;
    private const int ValueForHealthUp = 10;

    private readonly Vector3 _positionOffset = new Vector3(0.2f, 1f);

    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private Rigidbody _rigidbody;
    [FormerlySerializedAs("_enemiesMaker")] [FormerlySerializedAs("_enemiesController")] [SerializeField] private EnemiesWaveController _enemiesWaveController;
    [SerializeField] private UIAndGameController _uiAndGameController;
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Image _heathBar;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Text _currentDamageText;
    [SerializeField] private Text _currentAttackSpeedText;
    [SerializeField] private Text _currentHealthUpText;
    [SerializeField] private GameObject _dustEffectPrefab;
    [SerializeField] private GameObject _windEffectPrefab;
    [SerializeField] private float _health;
    [SerializeField] private float _speed;

    private float _movementOffset;
    private float _timerForForward;
    private float _timerSpawnEffectFlash;
    private float _timerDustEffectFlash;
    private float _maxHealth;
    private float _startSpeedAttack = 2;
    private int _damage;

    public event Action<float> GotHit;

    private void Awake()
    {
        _damage = StartDamage;
        _maxHealth = StartHealth;
        _health = _maxHealth;
    }
    
    private void Update()
    {
        if (_uiAndGameController.GameIsPaused)
        {
            return;
        }

        if (!_uiAndGameController.GameIsStarted)
        {
            return;
        }

        FindClosestEnemy();
        CheckSpawnEffectsTimer();
        CheckDustEffectsTimer();

        if (_movementOffset == 0 && _enemiesWaveController.Enemies.Count != 0)
        {
            StartAttack();
        }

    }
    
    private void FixedUpdate()
    {
        MovementController();
    }

    private void OnTriggerEnter(Collider other)
    {
        var trigger = other.GetComponent<TriggerToActivateEnemies>();
        
        if (trigger)
        {
            _enemiesWaveController.SpawnEnemiesWave();
        }
        
    }

    private Enemy FindClosestEnemy()
    {
        var distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (Enemy enemy in _enemiesWaveController.Enemies)
        {
            if (enemy.IsDead)
            {
                continue;
            }

            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                _currentEnemy = enemy;
                distance = curDistance;
            }
        }

        return _currentEnemy;
    }

    private void MovementController()
    {
        _rigidbody.AddForce(new Vector3(_movementOffset, 0.0f, 0.0f) * _speed, ForceMode.Impulse);

        if (_timerForForward > 0)
        {
            _timerForForward -= Time.deltaTime;

            if (_timerForForward > TimeWhenNeedToSlowDown)
            {
                _movementOffset = MovementOffsetRight;
            }
            else
            {
                _movementOffset = MovementOffsetLeft;
            }

            _playerAnimations.PlayRun(true);
        }
        else
        {
            _movementOffset = 0;
            _playerAnimations.PlayRun(false);
        }
    }

    private void CheckSpawnEffectsTimer()
    {
        if (_timerSpawnEffectFlash > 0)
        {
            _timerSpawnEffectFlash -= Time.deltaTime;
        }
        
        if (_timerSpawnEffectFlash < 0)
        {
            _timerSpawnEffectFlash = 0;

            SpawWindEffects();
        }
    }

    private void CheckDustEffectsTimer()
    {
        if (_timerDustEffectFlash > 0)
        {
            _timerDustEffectFlash -= Time.deltaTime;
        }
        
        if (_timerDustEffectFlash < 0)
        {
            _timerDustEffectFlash = 0;

            SpawDustEffects();
        }
    }

    private void StartAttack()
    {
        _playerAnimations.Attack();
    }

    private void SpawDustEffects()
    {
        var dust = Instantiate(_dustEffectPrefab, new Vector3(transform.position.x - 0.58f, transform.position.y + 0.14f,
              transform.position.z), Quaternion.identity);
        dust.transform.SetParent(_canvas.transform, true);
    }

    private void SpawWindEffects()
    {
        var dust = Instantiate(_windEffectPrefab, new Vector3 (0,0,-_positionOffset.y), Quaternion.identity);
        dust.transform.SetParent(_canvas.transform, false);
    }

    public void SpawnProjectile()
    {
        if (_currentEnemy != null)
        {
            var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            projectile.Launch(_currentEnemy.transform, _damage);
        }
    }

    public void StartMovement()
    {
        _timerForForward = MovementTime;
        _timerSpawnEffectFlash = SpawnFlashEffectTime;
        _timerDustEffectFlash = SpawnDustEffectTime;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _heathBar.fillAmount = _health / _maxHealth;

        _playerAnimations.Hurt();

        GotHit?.Invoke(damage);
        
        if(_health <= 0)
        {
            _uiAndGameController.AwakePanelYouDied();
            _playerAnimations.Death();
        }
    }

    public void HealthRecovery()
    {
        if (_health < _maxHealth)
        {
            _health += RecoveryHealthValue;
            _heathBar.fillAmount = _health / _maxHealth;
        }
    }

    public void UpHealth()
    {
        _maxHealth += ValueForHealthUp;
        _currentHealthUpText.text = _maxHealth.ToString();
    }

    public void UpDamage()
    {
        _damage++;
        _currentDamageText.text = _damage.ToString();
    }

    public void UpSpeedAttack()
    {
        _startSpeedAttack += AttackSpeedStep;

        _playerAnimations.CreateSpeedAttack(_startSpeedAttack);
        _currentAttackSpeedText.text = _startSpeedAttack.ToString("0.00");
    }

}
