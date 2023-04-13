using UnityEngine;
using UnityEngine.Serialization;

/// 1) Запускает движение игрока
public class LaunchPlayerMovement : MonoBehaviour
{
    [FormerlySerializedAs("_enemiesWaveController")] [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;
    [SerializeField] private PlayerMovement _playerMovementr;

    private void Update()
    {
        if (_enemiesWaveSpawner.Enemies.Count == 0 &&
            !_enemiesWaveSpawner.IsAllEnemiesInWaveDead &&
            _enemiesWaveSpawner.CounterWaves < _enemiesWaveSpawner.LastWaves)
        {
            _playerMovementr.StartMovement();
            _enemiesWaveSpawner.EnemiesInWaveDead();
        }
    }
}