using UnityEngine;

public class LaunchPlayerMovement : MonoBehaviour
{
    [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Start()
    {
        _enemiesWaveSpawner.LaunchPlayer += LaunchPlayer;
    }

    private void OnDestroy()
    {
        _enemiesWaveSpawner.LaunchPlayer -= LaunchPlayer;
    }
    
    private void LaunchPlayer()
    {
        _playerMovement.StartMovement();
    }
}