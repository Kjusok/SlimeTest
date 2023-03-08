using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private UIAndGameController _uiAndGameController;

    private int _counter;
    private int _lastEnemyNumber = 8;

    public IEnumerable<Enemy> Enemies => _enemies;


    private void Update()
    {
        if (_uiAndGameController.GameIsPaused)
        {
            return;
        }

        if (_enemies[_lastEnemyNumber].IsDead)
        {
            _uiAndGameController.AwakeFinishPanel();
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

    public void ActivateEnemies(int maxEnemies)
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            if (_enemies[i] != null)
            {
                _enemies[i].Activate();
            }
        }
    }
}
