using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 1) Находит близжайшего врага
/// </summary>
public class ClosestEnemySearch : MonoBehaviour
{
    [FormerlySerializedAs("_enemiesWaveController")] [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;
    
    public Enemy CurrentEnemy{ get; private set; }
    
    
    private void Update()
    {
        SearchClosestEnemy();
    }
    
    private Enemy SearchClosestEnemy()
    {
        var distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (Enemy enemy in _enemiesWaveSpawner.Enemies)
        {
            if (enemy.IsDead)
            {
                continue;
            }

            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                CurrentEnemy = enemy;
                distance = curDistance;
            }
        }

        return CurrentEnemy;
    }
}