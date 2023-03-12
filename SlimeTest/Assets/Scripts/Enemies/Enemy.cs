using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private const int BaseScore = 100;
    private const float NexHitDelay = 1;

    private readonly Vector3 _textOffset = new Vector3(0.2f, 0.5f);

    [SerializeField] private Player _target;
    [SerializeField] private PopUpText _prefabPopUpTextDamage;
    [SerializeField] private Image _heathBar;
    [SerializeField] private EnemiesController _enemiesController;
    [SerializeField] private UIAndGameController _uiAndGameController;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private EnemiesAnimations _enemiesAnimations;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _maxHealth = 100;

    private Player _player;
    private bool _isAttack;
    private bool _isActive;
    private float _timerToNextHit;
    private float _damage = 5;
    private float _heath;
    private int _score;

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

        if (!_isActive)
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
            _player = player;
            _isAttack = true;
            _enemiesAnimations.Attack(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            _player = null;
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

        if (_isAttack && _timerToNextHit <= 0)
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

            _enemiesController.CheckListEnemies();
            _uiAndGameController.AddCoinsToWallet(_score);

            Destroy(gameObject);
        }
    }

    public void Activate()
    {
        _isActive = true;
    }
}
