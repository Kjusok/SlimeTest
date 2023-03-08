using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const float TimeForMovement = 2.5f;
    private const float TimeWhenNeedToSlowDown = 0.7f;
    private const float MovementOffsetRight = 0.9f;
    private const float MovementOffsetLeft = -0.8f;
    private const float InitialValueForTimerShot = 0.5f;
    private const float MinValueForSpeedAttack = 0.1f;
    private const float AttackSpeedStep = 0.015f;
    private const int ValueForMaxIndex = 2;
    private const int MaxValueForMaxIndex = 9;
    private const int StartDamage = 10;
    private const int StartHealth = 100;
    private const int RecoveryHealthValue = 5;
    private const int ValueForHealthUp = 10;

    private readonly Vector3 _textOffset = new Vector3(0.2f, 0.5f);

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private EnemiesController _enemiesController;
    [SerializeField] private UIAndGameController _UIAndGameController;
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Image _heathBar;
    [SerializeField] private PopUpText _prefabPopUpTextDamage;
    [SerializeField] private PopUpText _prefabPopUPTextHealtUp;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Text _currentDamageText;
    [SerializeField] private Text _currentAttackSpeedText;
    [SerializeField] private Text _currentHealthUpText;
    [SerializeField] private float _health;
    [SerializeField] private float _speedForRotation;
    [SerializeField] private float _speed;

    private float _movementOffset;
    private float _timerForForward;
    private bool _isAttacked;
    private float _timerForNextHit;
    private float _maxHealth;
    private float _timeForNextShot;
    private int _counterWaves;
    private int _maxIndexForEnemiesTrigger;
    private int _damage;


    private void Start()
    {
        _damage = StartDamage;
        _maxHealth = StartHealth;
        _health = _maxHealth;
        _maxIndexForEnemiesTrigger = 0;
        _timeForNextShot = InitialValueForTimerShot;
    }

    private void Update()
    {
        if (_UIAndGameController.GameIsPaused)
        {
            return;
        }

        if(!_UIAndGameController.GameIsStarted)
        {
            return;
        }

        if (FindClosestEnemy())
        {
            RotationToClosestEnemy();
        }

        if (_movementOffset == 0 && _counterWaves > 0)
        {
            StartAttack();
        }

        CheckTimeForNextAttack();
    }

    private void FixedUpdate()
    {
        MovementController();
    }

    private void OnTriggerExit(Collider other)
    {
        var trigger = other.GetComponent<TriggerToActivateEnemies>();

        if (trigger)
        {
            _counterWaves++;
            _maxIndexForEnemiesTrigger += ValueForMaxIndex;

            if (_maxIndexForEnemiesTrigger > MaxValueForMaxIndex)
            {
                _maxIndexForEnemiesTrigger = MaxValueForMaxIndex;
            }

            _enemiesController.ActivateEnemies(_maxIndexForEnemiesTrigger);
        }
    }

    private Enemy FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (Enemy enemy in _enemiesController.Enemies)
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

    private void CheckTimeForNextAttack()
    {
        if (_timerForNextHit >= 0)
        {
            _timerForNextHit -= Time.deltaTime;
        }
        else
        {
            _isAttacked = false;
        }
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
        }
        else
        {
            _movementOffset = 0;
        }
    }

    private void SpawnProjectile()
    {
        var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        projectile.Initialize(_currentEnemy.transform, _damage);
    }

    private void ShowChangeHealth(float value, PopUpText prefab)
    {
        var text = Instantiate(prefab,
            new Vector3(transform.position.x + (Random.Range(-_textOffset.x, _textOffset.x)),
            transform.position.y + _textOffset.y,
            transform.position.z),
            transform.rotation);

        text.transform.SetParent(_canvas.transform, true);
        text.Initialize(value.ToString());
    }

    private void StartAttack()
    {
        if (!_isAttacked)
        {
            _isAttacked = true;
            _timerForNextHit = _timeForNextShot;

            SpawnProjectile();
        }
    }

    private void RotationToClosestEnemy()
    {
        Vector3 direction = _currentEnemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedForRotation * Time.deltaTime);
    }

    public void StartMovement()
    {
        _timerForForward = TimeForMovement;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _heathBar.fillAmount = _health / _maxHealth;

        ShowChangeHealth(-damage, _prefabPopUpTextDamage);

        if(_health <= 0)
        {
            _UIAndGameController.AwakePanelYouDied();
        }
    }

    public void HealthRecovery()
    {
        if (_health < _maxHealth)
        {
            _health += RecoveryHealthValue;
            _heathBar.fillAmount = _health / _maxHealth;

            ShowChangeHealth(RecoveryHealthValue, _prefabPopUPTextHealtUp);
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
        if (_timeForNextShot > MinValueForSpeedAttack)
        {
            _timeForNextShot -= AttackSpeedStep;
            _currentAttackSpeedText.text = _timeForNextShot.ToString("0.00");
        }
    }
}
