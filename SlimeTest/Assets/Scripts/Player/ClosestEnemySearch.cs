using UnityEngine;

public class ClosestEnemySearch : MonoBehaviour
{
    [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;
    
    public Enemy CurrentEnemy{ get; private set; }
    
    
    private void Update()
    {
        CurrentEnemy = SearchClosestEnemy();
    }
    
    private Enemy SearchClosestEnemy()
    {
        var distance = Mathf.Infinity;
        Vector3 position = transform.position;

        Enemy resultEnemy = null;
        
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
                resultEnemy = enemy;
                distance = curDistance;
            }
        }

        return resultEnemy;
    }
}