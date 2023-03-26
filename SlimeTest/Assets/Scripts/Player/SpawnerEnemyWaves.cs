using UnityEngine;
/// 1) Косаться трригера (Что бы создать волну врагов)

public class SpawnerEnemyWaves : MonoBehaviour
{
    [SerializeField] private EnemiesWaveController _enemiesWaveController;

    private void OnTriggerEnter(Collider other)
    {
        var trigger = other.GetComponent<FlagToActivateEnemies>();
        
        if (trigger)
        {
            _enemiesWaveController.SpawnEnemiesWave();
        }
    }
}