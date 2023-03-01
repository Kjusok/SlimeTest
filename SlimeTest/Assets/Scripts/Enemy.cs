using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Slime _target;
    [SerializeField] private float _speedForRotation;
    [SerializeField] private float _speedForMovement;
    [SerializeField] private PopUpText _prefabText;
    [SerializeField] private float _heath;
    [SerializeField] private Image _heathBar;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Canvas _canvas;

    private bool _targetCatched = false;
    private float _timerToNextHit;
    private float _damage = 5;
    private bool IsAtacked;
    private int _coins;

    public bool IsDead
    {
        get; private set;
    }

    private void Start()
    {
        _timerToNextHit = 1;
        _coins = 100;
    }



    private void OnTriggerStay(Collider other)
    {
        var trigger = other.GetComponent<Slime>();
        

        if (trigger)
        {
            _targetCatched = true;

            if (!IsAtacked)
            {
                IsAtacked = true;
                trigger.TakeDamage(_damage);
                _timerToNextHit = 1;
            }
        }

        
    }
    public void RotationToClosestEnemy()
    {
        if (!IsDead)
        {
            Vector3 direction = _target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedForRotation * Time.deltaTime);

        }


    }

    private void InstantiatePopUpText(float damage)
    {
        var text = Instantiate(_prefabText, new Vector3(transform.position.x + (Random.Range(-0.2f, 0.2f)), transform.position.y + 0.5f, transform.position.z), transform.rotation);
        text.transform.SetParent(_canvas.transform, true);
        text.Initialize("-" + damage.ToString());
    }

    public void MovementToTarget()
    {
        if (!_targetCatched)
        {
            transform.Translate(Vector3.forward * _speedForRotation * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        _heath -= damage;

        InstantiatePopUpText( damage);
        _heathBar.fillAmount = _heath / 100;

        if(_heath <= 0)
        {
            IsDead = true;

            _enemyController.CheckListEnemies();
            _gameManager.AddCoinsToCash(_coins);

            
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_gameManager.GameIsPaused)
        {
            return;
        }

        if (_timerToNextHit >= 0)
        {
            _timerToNextHit -= Time.deltaTime;
        }
        else
        {
            IsAtacked = false;
        }
    }
}
