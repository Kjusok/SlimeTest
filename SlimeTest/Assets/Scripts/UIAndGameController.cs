using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIAndGameController : MonoBehaviour
{
    private const int PriceForCharacteristics = 25;
    private const int PriceForRecoverHP = 10;

    [SerializeField] private Player _player;
    [SerializeField] private Text _walletText;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _panelYouDied;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _panelYouWin;

    private int _wallet;
    private bool _isPaused;

    public bool GameIsPaused => _isPaused;
    public bool GameIsStarted
    {
        get; private set;
    }


    private void RemoveFromScore(int coins)
    {
        _wallet -= coins;
        string scoreTextWithZero = string.Format("{0:D6}", _wallet);

        _walletText.text = scoreTextWithZero;
    }

    public void AwakePanelYouDied()
    {
        _isPaused = true;
        _panelYouDied.SetActive(true);
        _restartButton.SetActive(true);
    }

    public void AwakeFinishPanel()
    {
        _isPaused = true;
        _restartButton.SetActive(true);
        _panelYouWin.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AddCoinsToWallet(int coins)
    {
        _wallet += coins;
        string scoreTextWithZero = string.Format("{0:D6}", _wallet);

        _walletText.text = scoreTextWithZero;
    }

    public void UpDamageButton()
    {
        if(_wallet >= PriceForCharacteristics)
        {
            _player.UpDamage();
            RemoveFromScore(PriceForCharacteristics);
        }
    }

    public void UpSpeedAtackButton()
    {
        if (_wallet >= PriceForCharacteristics)
        {
            _player.UpSpeedAttack();
            RemoveFromScore(PriceForCharacteristics);
        }
    }

    public void UpHealthButton()
    {
        if (_wallet >= PriceForCharacteristics)
        {
            RemoveFromScore(PriceForCharacteristics);
            _player.UpHealth();
        }
    }

    public void RecoveryHealthButton()
    {
        if (_wallet >= PriceForRecoverHP)
        {
            _player.RecoveryHP();
            RemoveFromScore(PriceForRecoverHP);
        }
    }

    public void PressStartButton()
    {
        _player.StartMovement();
        GameIsStarted = true;
        _startButton.SetActive(false);
    }
}
