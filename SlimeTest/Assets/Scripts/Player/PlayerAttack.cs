using UnityEngine;
/// <summary>
/// Атака игрока
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SpawnProjectiles _spawnProjectiles;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private EnemiesWaveController _enemiesWaveController;
    [SerializeField] private PlayerAnimations _playerAnimations;


    public void Attack()
    {
        _spawnProjectiles.SpawnProjectile();
    }

    private void Update()
    {
        if (_playerMovement.MovementOffset == 0 && _enemiesWaveController.Enemies.Count != 0)
        {
            _playerAnimations.Attack();
        }
    }
}
