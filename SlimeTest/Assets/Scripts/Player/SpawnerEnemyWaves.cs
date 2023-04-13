using UnityEngine;
using UnityEngine.Serialization;

/// 1) Косаться трригера (Что бы создать волну врагов)

public class SpawnerEnemyWaves : MonoBehaviour
{
    [FormerlySerializedAs("_enemiesWaveController")] [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;

    private void OnTriggerEnter(Collider other)
    {
        var trigger = other.GetComponent<TagToActivateEnemies>();
        
        if (trigger)
        {
            _enemiesWaveSpawner.SpawnEnemiesWave();
        }
    }
}