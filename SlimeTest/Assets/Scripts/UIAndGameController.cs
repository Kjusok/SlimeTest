using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 1) Активация кнопок в игре
/// 2) Добовлять и убирать очки (монетки) из кошелька
/// 3) Перезагружать сцену
/// 4) Активировать различные панели (победа, проигрышь, старт игры)
/// 5) увеличивать различные характериски игрока после нажатия кнопки (повышение жизни, пополнение здоровья, увилечение урона, увелечение скорости атаки)
/// </summary>
public class UIAndGameController : MonoBehaviour
{
    private const int PriceForCharacteristics = 25;
    private const int PriceForRecoverHealth = 10;

    [SerializeField] private Player _player;
    [SerializeField] private Text _walletText;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _panelYouDied;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _panelYouWin;
    [SerializeField] private Button _upDamageButton;
    [SerializeField] private Button _upHealthButton;
    [SerializeField] private Button _upSpeedAttackButton;
    [SerializeField] private Button _recoveryHealthButton;

    private int _wallet;
    private bool _isPaused;

    public bool GameIsPaused => _isPaused;
    public bool GameIsStarted { get; private set; }


    private void Update()
    {
        CheckAvailableButtons();
    }

    private void RemoveFromScore(int coins)
    {
        _wallet -= coins;
        string scoreTextWithZero = string.Format("{0:D6}", _wallet);

        _walletText.text = scoreTextWithZero;
    }

    private void CheckAvailableButtons()
    {
        if (_wallet >= PriceForCharacteristics)
        {
            _upDamageButton.interactable = true;
            _upHealthButton.interactable = true;
            _upSpeedAttackButton.interactable = true;
        }
        else
        {
            _upDamageButton.interactable = false;
            _upHealthButton.interactable = false;
            _upSpeedAttackButton.interactable = false;
        }

        if (_wallet >= PriceForRecoverHealth)
        {
            _recoveryHealthButton.interactable = true;
        }
        else
        {
            _recoveryHealthButton.interactable = false;
        }
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
        if (_wallet >= PriceForCharacteristics)
        {
            _player.UpDamage();
            RemoveFromScore(PriceForCharacteristics);
        }
    }

    public void UpSpeedAttackButton()
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
        if (_wallet >= PriceForRecoverHealth)
        {
            _player.HealthRecovery();
            RemoveFromScore(PriceForRecoverHealth);
        }
    }

    public void PressStartButton()
    {
        _player.StartMovement();
        GameIsStarted = true;
        _startButton.SetActive(false);
    }
}
