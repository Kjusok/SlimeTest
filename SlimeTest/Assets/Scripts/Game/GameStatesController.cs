using UnityEngine;
/// 1) Контроль состояния игры (Победа, поражение, игра начата, игра на паузе и т. д.)

public class GameStatesController : MonoBehaviour
{
    [SerializeField] private EnemiesWaveController _enemiesWaveController;
    [SerializeField] private GameObject _panelYouDied;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _panelYouWin;

    
    private bool _isPaused;
    
    public bool GameIsStarted { get; private set; }

    private void Update()
    {
        if (_enemiesWaveController.CounterWaves == _enemiesWaveController.LastWaves &&
            _enemiesWaveController.Enemies.Count == 0)
        {
            FinishGame();
        }
    }

    public void GameStart()
    {
        GameIsStarted = true;
    }

    public void PlayerDead()
    {
        _isPaused = true;
        _panelYouDied.SetActive(true);
        _restartButton.SetActive(true);
    }

    private void FinishGame()
    {
        _isPaused = true;
        _restartButton.SetActive(true);
        _panelYouWin.SetActive(true);
    }
}