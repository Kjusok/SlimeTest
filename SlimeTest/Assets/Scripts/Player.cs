using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const float TimeForMovement = 2.5f;
    private const float TimeWhenNeedToSlowDown = 0.7f;
    private const float MaxIndexForMove = 0.9f;
    private const float MinIndexForMove = -0.8f;
    private const float InitialValueForTimerShot = 0.5f;
    private const float CoefForPosX = 0.2f;
    private const float CoefForPosY = 0.5f;
    private const float MinValueForSpeedAttack = 0.1f;
    private const float AttackSpeedStep = 0.015f;
    private const int ValueForMaxIndex = 2;
    private const int MaxValueForMaxIndex = 9;
    private const int InitialValueOfDamage = 10;
    private const int InitialValueOfHealth = 100;
    private const int ValueForRecoverHP = 5;
    private const int ValueForHealthUp = 10;
    private const int LeadToHundredths = 100;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private EnemyController _enemyController;
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

    private float _movementIndex;
    private float _timerForForward;
    private bool _isAttacked;
    private float _timerForNextHit;
    private float _maxHealth;
    private float _timeForNextShot;
    private int _counterTrigger;
    private int _maxIndexForEnemiesTrigger;
    private int _damage;


    private void Start()
    {
        _damage = InitialValueOfDamage;
        _maxHealth = InitialValueOfHealth;
        _health = _maxHealth;
        _maxIndexForEnemiesTrigger = 0;
        _timeForNextShot = InitialValueForTimerShot;

        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezeRotationY;
    }

    private void Update()
    {
        if (_UIAndGameController.GameIsPaused)
        {
            return;
        }

        if (FindClosestEnemy())
        {
            RotationToClosestEnemy();
        }

        if (_movementIndex == 0 && _UIAndGameController.GameIsStarted && _counterTrigger > 0)
        {
            StartAttack();
        }

        ChechTimeForNextAttack();
    }

    private void FixedUpdate()
    {
        MovementController();
    }

    private void OnTriggerExit(Collider other)
    {
        var trigger = other.GetComponent<Trigger>();

        if (trigger)
        {
            _counterTrigger++;
            _maxIndexForEnemiesTrigger += ValueForMaxIndex;

            if (_maxIndexForEnemiesTrigger > MaxValueForMaxIndex)
            {
                _maxIndexForEnemiesTrigger = MaxValueForMaxIndex;
            }

            _enemyController.RotationAndMovementToTarget(_maxIndexForEnemiesTrigger);
        }
    }

    private Enemy FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (Enemy enemy in _enemyController.Enemies)
        {
            if (!enemy.IsDead)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    _currentEnemy = enemy;
                    distance = curDistance;
                }
            }
        }

        return _currentEnemy;
    }

    private void ChechTimeForNextAttack()
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
        _rigidbody.AddForce(new Vector3(_movementIndex, 0.0f, 0.0f) * _speed, ForceMode.Impulse);

        if (_timerForForward > 0)
        {
            _timerForForward -= Time.deltaTime;

            if (_timerForForward > TimeWhenNeedToSlowDown)
            {
                _movementIndex = MaxIndexForMove;
            }
            else
            {
                _movementIndex = MinIndexForMove;
            }
        }
        else
        {
            _movementIndex = 0;
        }
    }

    private void SpawnProjectile()
    {
        var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().Damage = _damage;
        projectile.GetTarget(_currentEnemy.transform);
    }

    private void InstantiatePopUpText(float value, string symbol, PopUpText prefab)
    {
        var text = Instantiate(prefab,
            new Vector3(transform.position.x + (Random.Range(-CoefForPosX, CoefForPosX)),
            transform.position.y + CoefForPosY,
            transform.position.z),
            transform.rotation);

        text.transform.SetParent(_canvas.transform, true);
        text.Initialize(symbol + value.ToString());
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

    public void StartMovement()
    {
        _timerForForward = TimeForMovement;
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

    public void RotationToClosestEnemy()
    {
        Vector3 direction = _currentEnemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedForRotation * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _heathBar.fillAmount = _health / LeadToHundredths;

        InstantiatePopUpText(damage, "-", _prefabPopUpTextDamage);

        if(_health <= 0)
        {
            _UIAndGameController.AwakePanelYouDied();
        }
    }

    public void RecoveryHP()
    {
        if (_health < _maxHealth)
        {
            _health += ValueForRecoverHP;
            _heathBar.fillAmount = _health / LeadToHundredths;

            InstantiatePopUpText(ValueForRecoverHP, "+", _prefabPopUPTextHealtUp);
        }
    }

    public void UpHealth()
    {
        _maxHealth += ValueForHealthUp;
        _currentHealthUpText.text = _maxHealth.ToString();
    }
}
