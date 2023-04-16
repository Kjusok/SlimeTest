using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private ClosestEnemySearch _closestEnemySearch;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Player _player;

    
    /// <summary>
    /// This method use from Animations event
    /// </summary>
    public void Attack()
    {
        SpawnProjectile();
    }

    private void Update()
    {
        if (_playerMovement.MovementOffset == 0 && _enemiesWaveSpawner.Enemies.Count != 0)
        {
            _playerAnimations.Attack();
        }
    }
    
    private void SpawnProjectile()
    {
        if (_closestEnemySearch.CurrentEnemy)
        {
            var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            projectile.Launch(_closestEnemySearch.CurrentEnemy.transform, _player.StatsController.Damage);
        }
    }
}
