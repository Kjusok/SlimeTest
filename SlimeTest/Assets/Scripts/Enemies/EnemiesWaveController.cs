using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 1) Создает волны врагов
/// 2) Считает волны
/// 3) Выбирает тип врага
/// </summary>
public class EnemiesWaveController : MonoBehaviour
{
    private const int ManValueEnemies = 3;
    private const float OffsetY = 0.15f;
    private const float OffsetMinX = 44;
    private const float OffsetMaxX= 44.5f;
    private const float OffsetMinZ= -3f;
    private const float OffsetMaxZ= 5f;
    
    [SerializeField] private Player _player;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Enemy _bossPrefab;

    private int _enemiesInWave;
    private Enemy _typeOfEnemy;
    private float _offsetY;

    public readonly int LastWaves  = 5;

    public int CounterWaves { get; private set; }
    public bool IsAllEnemiesInWaveDead { get; private set; } = true;
    public List<Enemy> Enemies => _enemies;

    
    private void CheckCharacteristicsEnemy()
    {
        if (CounterWaves == LastWaves)
        {
            _enemiesInWave = 0;
            _typeOfEnemy = _bossPrefab;
            _offsetY = OffsetY;
        }
        else
        {
            _enemiesInWave = Random.Range(0, ManValueEnemies);
            _typeOfEnemy = _enemyPrefab;
            _offsetY = -OffsetY;
        }
    }
    
    public void SpawnEnemiesWave()
    {
        IsAllEnemiesInWaveDead = false;
        CounterWaves++;

        CheckCharacteristicsEnemy();
        
        for (int i = 0; i <= _enemiesInWave; i++)
        {
            var enemy = Instantiate(_typeOfEnemy, new Vector3(_player.transform.position.x - Random.Range(OffsetMinX,OffsetMaxX),
                _offsetY,
                Random.Range(OffsetMinZ,OffsetMaxZ)),
                Quaternion.identity).GetComponent<Enemy>();
            
            enemy.Dead += RemoveEnemyFromList;
            
            enemy.GetComponent<EnemiesMovementController>().Initialize(_player);
            enemy.GetComponent<EnemyAttack>().Initialize(_player);
            enemy.transform.SetParent(transform, false);
            
            _enemies.Add(enemy);
        }
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        enemy.Dead -= RemoveEnemyFromList;
        
        _wallet.Coins += enemy.Score;

        _enemies.Remove(enemy);
    }

    public void EnemiesInWaveDead()
    {
        IsAllEnemiesInWaveDead = true;
    }
}


