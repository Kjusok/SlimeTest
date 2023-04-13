using System;
using UnityEngine;
using UnityEngine.Serialization;

/// 1) Контроль состояния игры (Победа, поражение, игра начата, игра на паузе и т. д.)

public class GameStatesController : MonoBehaviour
{
    [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;
    [SerializeField] private GameObject _panelYouDied;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _panelYouWin;


    private void Start()
    {
        _enemiesWaveSpawner.WavesFinished += OnWavesFinished;
    }
    // private bool GameIsStarted;

   private void OnWavesFinished()
   {
       FinishGame();
   }

   private void OnDestroy()
   {
       _enemiesWaveSpawner.WavesFinished -= OnWavesFinished;
   }
   // private void Update()
    // {
    //     if (_enemiesWaveSpawner.CounterWaves == _enemiesWaveSpawner.LastWaves &&
    //         _enemiesWaveSpawner.Enemies.Count == 0)
    //     {
    //         FinishGame();
    //     }
    // }

    // public void GameStart()
    // {
    //     GameIsStarted = true;
    // }

    public void PlayerDead()
    {
        _panelYouDied.SetActive(true);
        _restartButton.SetActive(true);
    }

    private void FinishGame()
    {
        _restartButton.SetActive(true);
        _panelYouWin.SetActive(true);
    }
}