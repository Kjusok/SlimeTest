using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 1) Соприкосновение с играком
/// 2) Способность атаковать
/// 3) Перемещаться к цели (движение и вращение)
/// 4) Показывать урон
/// 5) Получать урон
/// 6) Активироваться 
/// </summary>
public class Enemy : MonoBehaviour
{
    private const int BaseScore = 100;
    private const float NexHitDelay = 1;

    [SerializeField] private Player _target;
    [SerializeField] private PopUpText _prefabPopUpTextDamage;
    [SerializeField] private Image _heathBar;
    [SerializeField] private EnemiesController _enemiesController;
    [SerializeField] private UIAndGameController _uiAndGameController;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private EnemiesAnimations _enemiesAnimations;
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _damage = 5;
    [SerializeField] private GameObject _deathPrefab;

    private readonly Vector3 _textOffset = new Vector3(0.2f, 0.5f);

    private bool _isAttack;
    private float _timerToNextHit;
    private float _heath;
    private int _score;

    public void Initialize(UIAndGameController UI, Player target, EnemiesController enemiesController)
    {
        _uiAndGameController = UI;
        _target = target;
        _enemiesController = enemiesController;
    }

    public bool IsDead { get; private set; }
   

    private void Start()
    {
        _score = BaseScore;
        _heath = _maxHealth;
    }

    private void Update()
    {
        if (_uiAndGameController.GameIsPaused)
        {
            return;
        }

        RotationToTarget();
        MovementToTarget();
        AttackCalculation();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            _isAttack = true;
            _enemiesAnimations.Attack(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            _isAttack = false;
            _enemiesAnimations.Attack(false);
        }
    }

    private void AttackCalculation()
    {
        if (_timerToNextHit >= 0)
        {
            _timerToNextHit -= Time.deltaTime;
        }

        if (_isAttack && _timerToNextHit < 0)
        {
            _target.TakeDamage(_damage);
            _timerToNextHit = NexHitDelay;
        }
    }

    private void RotationToTarget()
    {
        if(IsDead || _isAttack)
        {
            return;
        }

        Vector3 direction = _target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }

    private void MovementToTarget()
    {
        if (_isAttack)
        {
            _enemiesAnimations.Walk(false);
            return;
        }

        _enemiesAnimations.Walk(true);
        transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
    }

    private void ShowDamage(float damage)
    {
        var text = Instantiate(_prefabPopUpTextDamage,
            new Vector3(transform.position.x + (Random.Range(-_textOffset.x, _textOffset.x)), transform.position.y + _textOffset.y, transform.position.z),
           Quaternion.identity);
        text.transform.SetParent(_canvas.transform, true);
        text.Initialize($"{ damage}");
    }

    public void TakeDamage(int damage)
    {
        _heath -= damage;

        ShowDamage(damage);
        _enemiesAnimations.Hurt();

        _heathBar.fillAmount = _heath / _maxHealth;

        if (_heath <= 0)
        {
            IsDead = true;

            _uiAndGameController.AddCoinsToWallet(_score);
            
            _enemiesController.RemoveEnemyFromList(this);
            Destroy(gameObject);
            
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }

   
}
