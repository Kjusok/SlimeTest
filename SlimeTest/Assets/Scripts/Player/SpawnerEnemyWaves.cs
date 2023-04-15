using UnityEngine;

public class SpawnerEnemyWaves : MonoBehaviour
{
    [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TagToActivateEnemies>())
        {
            _enemiesWaveSpawner.SpawnEnemiesWave();
        }
    }
}