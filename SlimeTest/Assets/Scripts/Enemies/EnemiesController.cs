using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 1) Проверяет кол-во врагов на сцене (Запускает движение игрока)
/// 2) Включает панель перезапуска игры в случае победы (уничтожения всех врагов)
/// 3) Активирует врагов
/// </summary>
public class EnemiesController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private UIAndGameController _uiAndGameController;

    private int _counter;
    private Enemy _lastEnemy => _enemies[_enemies.Count-1];

    public IEnumerable<Enemy> Enemies => _enemies;


    private void Update()
    {
        if (_uiAndGameController.GameIsPaused)
        {
            return;
        }

        if (_lastEnemy.IsDead)
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
