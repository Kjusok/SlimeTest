using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 1) Проверяет кол-во врагов на сцене (Запускает движение игрока)
/// 2) Включает панель перезапуска игры в случае победы (уничтожения всех врагов)
/// 3) Активирует врагов
/// </summary>
public class EnemiesController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private UIAndGameController _uiAndGameController;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Enemy _bossPrefab;

    private int _counterWaves;
    private bool _isAllEnemiesInWaveDead = true;
    public List<Enemy> Enemies => _enemies;

    
    public void SpawnEnemiesWave()
    {
        var enemiesInWave = Random.Range(1, 3);
        var typeOfEnemy = _enemyPrefab;
        var offsetY = -0.15f;
        
        _isAllEnemiesInWaveDead = false;
        _counterWaves++;

        if (_counterWaves == 5)
        {
            enemiesInWave = 0;
            typeOfEnemy = _bossPrefab;
            offsetY = 0.15f;
        }
        
        for (int i = 0; i <= enemiesInWave; i++)
        {
            var enemy = Instantiate(typeOfEnemy, new Vector3(_player.transform.position.x - 43.5f-Random.Range(0.5f,1f), offsetY, Random.Range(-3f,5f)), Quaternion.identity);
            enemy.GetComponent<Enemy>().Initialize(_uiAndGameController,_player, this);
            enemy.transform.SetParent(transform, false);
            _enemies.Add(enemy);
        }
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
    
    private void Update()
    {
        if (_uiAndGameController.GameIsPaused)
        {
            return;
        }
        if (_enemies.Count == 0 && !_isAllEnemiesInWaveDead && _counterWaves < 5)
        {
            _player.StartMovement();
            _isAllEnemiesInWaveDead = true;
        }
        if (_counterWaves == 5 && _enemies.Count == 0)
        {
            _uiAndGameController.AwakeFinishPanel();
        }
    }
}
