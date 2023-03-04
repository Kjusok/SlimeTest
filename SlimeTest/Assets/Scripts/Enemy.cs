using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private const int ScorsInsade = 100;
    private const int LeadToHundredths = 100;
    private const float TimeForNextHit = 1;
    private const float CoefForPosX = 0.2f;
    private const float CoefForPosY = 0.5f;

    [SerializeField] private Player _target;
    [SerializeField] private PopUpText _prefabPopUpTextDamage;
    [SerializeField] private Image _heathBar;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private UIAndGameController _UIAndGameController;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _speedForRotation;
    [SerializeField] private float _speedForMovement;
    [SerializeField] private float _heath;

    private bool _targetCatched = false;
    private bool IsAtacked;
    private bool _isActive;
    private float _timerToNextHit = 1;
    private float _damage = 5;
    private int _score;

    public bool IsDead
    {
        get; private set;
    }
   

    private void Start()
    {
        _score = ScorsInsade;
    }

    private void Update()
    {
        if (_UIAndGameController.GameIsPaused)
        {
            return;
        }

        if (_isActive)
        {
            RotationToTarget();
            MovementToTarget();
        }

        CheckTimeForNextAtack();
    }
    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            _targetCatched = true;

            if (!IsAtacked)
            {
                IsAtacked = true;
                player.TakeDamage(_damage);

                _timerToNextHit = TimeForNextHit;
            }
        }
    }

    private void CheckTimeForNextAtack()
    {
        if (_timerToNextHit >= 0)
        {
            _timerToNextHit -= Time.deltaTime;
        }
        else
        {
            IsAtacked = false;
        }
    }

    private void RotationToTarget()
    {
        if (!IsDead)
        {
            Vector3 direction = _target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedForRotation * Time.deltaTime);
        }
    }

    private void MovementToTarget()
    {
        if (!_targetCatched)
        {
            transform.Translate(Vector3.forward * _speedForRotation * Time.deltaTime);
        }
    }

    private void InstantiatePopUpText(float damage)
    {
        var text = Instantiate(_prefabPopUpTextDamage,
            new Vector3(transform.position.x + (Random.Range(-CoefForPosX, CoefForPosX)), transform.position.y + CoefForPosY, transform.position.z),
            transform.rotation);
        text.transform.SetParent(_canvas.transform, true);
        text.Initialize("-" + damage.ToString());
    }

    public void TakeDamage(int damage)
    {
        _heath -= damage;

        InstantiatePopUpText(damage);
        _heathBar.fillAmount = _heath / LeadToHundredths;

        if (_heath <= 0)
        {
            IsDead = true;

            _enemyController.CheckListEnemies();
            _UIAndGameController.AddCoinsToWallet(_score);

            Destroy(gameObject);
        }
    }

    public void ActivateEnemy()
    {
        _isActive = true;
    }
}
