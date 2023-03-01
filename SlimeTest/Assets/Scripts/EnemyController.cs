using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Slime _slime;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private GameManager _gameManager;


    private int _counter;
    public IEnumerable<Enemy> Enemies => _enemies;
    
    

   
    public void CheckListEnemies()
    {
        _counter++;
        if(_counter % 2 == 0)
        {
            _slime.StartMovement();
        }
    }
    //public void RotationAfterTrigger( int counter, int minIndex, int maxindex )
    //{
    //    if (_slime._counterTrigger == 1)
    //    {
    //        for (int i = 0; i < 2; i++)
    //        {
    //            _enemy[i].RotationToClosestEnemy();
    //        }
    //    }
    //}

    private void Update()
    {
        


        if (_gameManager.GameIsPaused)
        {
            return;
        }
        //CheckListEnemies();

        if (_slime._counterTrigger == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                if(_enemies[i] != null)
                {
                    _enemies[i].RotationToClosestEnemy();
                    _enemies[i].MovementToTarget();
                }
                
            }
        }

        if (_slime._counterTrigger == 2)
        {
            for (int i = 2; i < 4; i++)
            {
                if(_enemies[i] != null)
                {
                    _enemies[i].RotationToClosestEnemy();
                    _enemies[i].MovementToTarget();
                }
            }
        }

        if (_slime._counterTrigger == 3)
        {
            for (int i = 4; i < 6; i++)
            {
                if(_enemies[i] != null)
                {
                    _enemies[i].RotationToClosestEnemy();
                    _enemies[i].MovementToTarget();
                }
            }
        }

        if (_slime._counterTrigger == 4)
        {
            for (int i = 6; i < 8; i++)
            {
                if (_enemies[i] != null)
                {
                    _enemies[i].RotationToClosestEnemy();
                    _enemies[i].MovementToTarget();
                }
            }
        }

        if (_slime._counterTrigger == 5)
        {
            for (int i = 8; i < 9; i++)
            {
                if (_enemies[i] != null)
                {
                    _enemies[i].RotationToClosestEnemy();
                    _enemies[i].MovementToTarget();
                }

            }


            if (_enemies[8] == null)
            {
                _gameManager.AwakeFinishPanel();
            }

        }
    }
}
