using UnityEngine;

public class LaunchPlayerMovement : MonoBehaviour
{
    [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;
    [SerializeField] private PlayerMovement _playerMovement;

    
    private void Start()
    {
        _enemiesWaveSpawner.WaveFinished += WaveFinished;
    }

    private void OnDestroy()
    {
        _enemiesWaveSpawner.WaveFinished -= WaveFinished;
    }
    
    private void WaveFinished()
    {
        _playerMovement.StartMovement();
    }
}