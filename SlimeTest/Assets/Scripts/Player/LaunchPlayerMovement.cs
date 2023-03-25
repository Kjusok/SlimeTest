using UnityEngine;
/// 1) Запускает движение игрока
public class LaunchPlayerMovement : MonoBehaviour
{
    [SerializeField] private EnemiesWaveController _enemiesWaveController;
    [SerializeField] private Player _player;

    private void Update()
    {
        if (_enemiesWaveController.Enemies.Count == 0 &&
            !_enemiesWaveController.IsAllEnemiesInWaveDead &&
            _enemiesWaveController.CounterWaves < _enemiesWaveController.LastWaves)
        {
            _player.StartMovement();
            _enemiesWaveController.EnemiesInWaveDead();
        }
    }
}