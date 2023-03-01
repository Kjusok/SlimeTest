using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    private const float TimeForMovement = 2.5f;
    private const float TimeWhenNeedToSlowDown = 0.7f;
    private const float MaxIndexForMove = 0.9f;
    private const float MinIndexForMove = -0.8f;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speedForRotation;
    [SerializeField] private float _speed;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Image _heathBar;
    [SerializeField] private PopUpText _prefabText;
    [SerializeField] private PopUpText _prefabTextGreen;
    [SerializeField] private Canvas _canvas;

    [SerializeField] private Text _currentDamageText;
    [SerializeField] private Text _currentAtackSpeedText;
    [SerializeField] private Text _currentHealthUpText;

    [SerializeField] private float _health;
    private float _monementIndex;
    private float _timerForForward;
    private bool _isAtacked;
    private float _timerForNextHit;
    private float _maxHealth;

    //public bool IsDied
    //{
    //    get; private set;
    //}
    [SerializeField] private float _timeForNextShot;
    public int _counterTrigger
    {
        get; private set;
    }

    [SerializeField] private int _damage;
    

    private void Start()
    {
        _damage = 10;
        _maxHealth = 100;
        _health = _maxHealth;

        _timeForNextShot = 0.5f;

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }

    public void StartMovement()
    {
        _timerForForward = TimeForMovement;

    }

    private void Update()
    {
        if (_gameManager.GameIsPaused)
        {
            return;
        }

        if (FindClosestEnemy())
        {
            RotationToClosestEnemy();
        }


        Debug.Log(_timerForForward);
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    InstantiatePopUpText();
        //}

        

        if (_monementIndex == 0 && _gameManager.GameIsStarted)
        {
            StartAtack();
        }

        if (_timerForNextHit >= 0)
        {
            _timerForNextHit -= Time.deltaTime;
        }
        else
        {
            _isAtacked = false;
        }

    }

    public void UpDamage()
    {
        _damage++;
        _currentDamageText.text = _damage.ToString();
    }

    public void UpSpeedAtack()
    {
        if (_timeForNextShot > 0.1f)
        {
            _timeForNextShot -= 0.015f;
            _currentAtackSpeedText.text = _timeForNextShot.ToString("0.00");
            

        }
    }

    private void StartAtack()
    {


        if (!_isAtacked)
        {
            _isAtacked = true;
            SpawnLaserBullet();

            _timerForNextHit = _timeForNextShot;
        }


    }

    public void RotationToClosestEnemy()
    {
        Vector3 direction = _currentEnemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedForRotation * Time.deltaTime);


    }

    private void InstantiatePopUpText(float damage)
    {
        var text = Instantiate(_prefabText, new Vector3(transform.position.x + (Random.Range(-0.2f, 0.2f)), transform.position.y + 0.5f, transform.position.z), transform.rotation);
        text.transform.SetParent(_canvas.transform, true);
        text.Initialize("-" + damage.ToString());
    }

    private void InstantiatePopUpTextGreen(float HP)
    {
        var text = Instantiate(_prefabTextGreen, new Vector3(transform.position.x + (Random.Range(-0.2f, 0.2f)), transform.position.y + 0.5f, transform.position.z), transform.rotation);
        text.transform.SetParent(_canvas.transform, true);
        text.Initialize( HP.ToString());
    }

    private void SpawnLaserBullet()
    {
        //if (_isShooting)
        //{
        var bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Shot>().Damage = _damage;
        //bullet.GetComponent<Shot>().Damage = _damage;

        //SpawnSparksFromBarrelGun();
        //}

        //_isShooting = true;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        InstantiatePopUpText(damage);

        _heathBar.fillAmount = _health / 100;

        if(_health <= 0)
        {
            _gameManager.AwakePanelYouDied();
           
        }

    }

    public void RecoveryHP()
    {
        if (_health < _maxHealth)
        {
            _health += 5;
            InstantiatePopUpTextGreen(5);
            _heathBar.fillAmount = _health / 100;
        }
          
    }

    public void UpHealth()
    {
        _maxHealth += 10;
        _currentHealthUpText.text = _maxHealth.ToString();

    }
    private Enemy FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (Enemy enemy in _enemyController.Enemies)
        {
            if (!enemy.IsDead /*&& position != enemy.transform.position*/)
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
    private void OnTriggerExit(Collider other)
    {
        var trigger = other.GetComponent<Trigger>();

        if (trigger)
        {
            _counterTrigger++;
        }
    }
    private void FixedUpdate()
    {
        if (_gameManager.GameIsPaused)
        {
            return;
        }

        _rigidbody.AddForce(new Vector3(_monementIndex, 0.0f, 0.0f) * _speed, ForceMode.Impulse);

        if (_timerForForward > 0)
        {
            _timerForForward -= Time.deltaTime;

            if (_timerForForward > TimeWhenNeedToSlowDown)
            {
                _monementIndex = MaxIndexForMove;
            }
            else
            {
                _monementIndex = MinIndexForMove;
            }
        }
        else
        {
            _monementIndex = 0;
        }
    }
}
