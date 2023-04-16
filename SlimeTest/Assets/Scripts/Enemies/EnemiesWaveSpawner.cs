using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesWaveSpawner : MonoBehaviour
{
    private const int ManValueEnemies = 3;
    private const float OffsetY = 0.15f;
    private const float OffsetMinX = 44;
    private const float OffsetMaxX= 44.5f;
    private const float OffsetMinZ= -3f;
    private const float OffsetMaxZ= 5f;
    private const int LastWaves  = 5;

    [SerializeField] private Player _player;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Enemy _bossPrefab;

    private int _enemiesInWave;
    private Enemy _typeOfEnemy;
    private float _offsetY;
    private int _counterWaves;
    private bool _isAllEnemiesInWaveDead = true;
    
    public List<Enemy> Enemies => _enemies;
    
    public event Action WavesFinished;
    public event Action WaveFinished;


    public void SpawnEnemiesWave()
    {
        _isAllEnemiesInWaveDead = false;
        _counterWaves++;

        UpdateCharacteristicsEnemy();
        
        for (int i = 0; i <= _enemiesInWave; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(_typeOfEnemy,
            new Vector3(_player.transform.position.x - Random.Range(OffsetMinX,OffsetMaxX),
                _offsetY,
                Random.Range(OffsetMinZ,OffsetMaxZ)),
            Quaternion.identity).GetComponent<Enemy>();
            
        enemy.Dead += OnEnemyDead;

        enemy.Initialize(_player);
            
        enemy.transform.SetParent(transform, false);
            
        _enemies.Add(enemy);
    }

    private void UpdateCharacteristicsEnemy()
    {
        if (_counterWaves == LastWaves)
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

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        
        _player.Wallet.AddCoins(enemy.Score);

        _enemies.Remove(enemy);

        if (!_enemies.Any() && _counterWaves == LastWaves)
        {
            WavesFinished?.Invoke();
        }

        if (!_enemies.Any() && _counterWaves < LastWaves && !_isAllEnemiesInWaveDead)
        {
            WaveFinished?.Invoke();
            _isAllEnemiesInWaveDead = true;
        }
    }
}


