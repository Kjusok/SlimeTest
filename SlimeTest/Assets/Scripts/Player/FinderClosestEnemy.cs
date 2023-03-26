﻿using UnityEngine;
/// <summary>
/// 1) Находит близжайшего врага
/// </summary>
public class FinderClosestEnemy : MonoBehaviour
{
    [SerializeField] private EnemiesWaveController _enemiesWaveController;
    
    public Enemy CurrentEnemy{ get; private set; }
    
    
    private void Update()
    {
        FindClosestEnemy();
    }
    
    private Enemy FindClosestEnemy()
    {
        var distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (Enemy enemy in _enemiesWaveController.Enemies)
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