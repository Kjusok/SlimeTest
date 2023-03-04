using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private UIAndGameController _UIAndGameController;

    private int _counter;

    public IEnumerable<Enemy> Enemies => _enemies;


    private void Update()
    {
        if (_UIAndGameController.GameIsPaused)
        {
            return;
        }

        if (_enemies[8] == null)
        {
            _UIAndGameController.AwakeFinishPanel();
        }
    }

    public void CheckListEnemies()
    {
        _counter++;

        if (_counter % 2 == 0)
        {
            _player.StartMovement();
        }
    }

    public void RotationAndMovementToTarget(int maxI)
    {
        for (int i = 0; i < maxI; i++)
        {
            if (_enemies[i] != null)
            {
                _enemies[i].ActivateEnemy();
            }
        }
    }
}
