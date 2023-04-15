using UnityEngine;

public class GameStates : MonoBehaviour
{
    [SerializeField] private EnemiesWaveSpawner _enemiesWaveSpawner;
    [SerializeField] private GameObject _panelYouDied;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _panelYouWin;
    [SerializeField] private Player _player;


    private void Start()
    {
        _enemiesWaveSpawner.WavesFinished += OnWavesFinished;
        _player.Dead += PlayerDead;
    }

    private void OnDestroy()
    {
        _enemiesWaveSpawner.WavesFinished -= OnWavesFinished;
        _player.Dead -= PlayerDead;

    }

    private void OnWavesFinished()
    {
        FinishGame();
    }
    
    private void PlayerDead()
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