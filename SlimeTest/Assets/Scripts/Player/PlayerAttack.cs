using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Атака игрока
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SpawnProjectiles _spawnProjectiles;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;
    [SerializeField] private PlayerAnimations _playerAnimations;


    public void Attack()
    {
        _spawnProjectiles.SpawnProjectile();
    }

    private void Update()
    {
        if (_playerMovement.MovementOffset == 0 && _enemiesWaveSpawner.Enemies.Count != 0)
        {
            _playerAnimations.Attack();
        }
    }
}
