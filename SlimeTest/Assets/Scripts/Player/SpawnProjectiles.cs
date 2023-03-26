using UnityEngine;
/// <summary>
/// 1) Создает снаряды
/// </summary>
public class SpawnProjectiles : MonoBehaviour
{
    [SerializeField] private FinderClosestEnemy _finderClosestEnemy; 
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private StatsController _statsController;

    
    public void SpawnProjectile()
    {
        if (_finderClosestEnemy.CurrentEnemy != null)
        {
            var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            projectile.Launch(_finderClosestEnemy.CurrentEnemy.transform, _statsController.Damage);
        }
    }
}