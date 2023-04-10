using UnityEngine;

/// <summary>
/// 1) Создает снаряды
/// </summary>
public class SpawnProjectiles : MonoBehaviour
{
    [SerializeField] private ClosestEnemySearch _closestEnemySearch;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Player _player;
    
    public void SpawnProjectile()
    {
        if (_closestEnemySearch.CurrentEnemy != null)
        {
            var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            projectile.Launch(_closestEnemySearch.CurrentEnemy.transform, _player.StatsController.Damage);
        }
    }
}