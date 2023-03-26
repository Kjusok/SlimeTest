using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 1) Активация кнопок в игре
/// 2) увеличивать различные характериски игрока после нажатия кнопки (повышение жизни, пополнение здоровья, увилечение урона, увелечение скорости атаки)
/// </summary>
public class ButtonController : MonoBehaviour
{
    private const int PriceForCharacteristics = 25;
    private const int PriceForRecoverHealth = 10;

    [SerializeField] private SceneController _sceneController;
    [SerializeField] private GameStatesController _gameStatesController;
    [SerializeField] private StatsController _statsController;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private Button _upDamageButton;
    [SerializeField] private Button _upHealthButton;
    [SerializeField] private Button _upSpeedAttackButton;
    [SerializeField] private Button _recoveryHealthButton;


    private void Update()
    {
        CheckAvailableButtons();
    }

    private void CheckAvailableButtons()
    {
        if (_wallet.WalletСontent >= PriceForCharacteristics)
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

        if (_wallet.WalletСontent >= PriceForRecoverHealth)
        {
            _recoveryHealthButton.interactable = true;
        }
        else
        {
            _recoveryHealthButton.interactable = false;
        }
    }
    
    public void RestartButton()
    {
        _sceneController.LoadMainScene();
    }
    
    public void UpDamageButton()
    {
        if (_wallet.WalletСontent >= PriceForCharacteristics)
        {
            _statsController.UpDamage();
            _wallet.RemoveFromScore(PriceForCharacteristics);
        }
    }

    public void UpSpeedAttackButton()
    {
        if (_wallet.WalletСontent >= PriceForCharacteristics)
        {
            _statsController.UpSpeedAttack();
            _wallet.RemoveFromScore(PriceForCharacteristics);
        }
    }

    public void UpHealthButton()
    {
        if (_wallet.WalletСontent >= PriceForCharacteristics)
        {
            _wallet.RemoveFromScore(PriceForCharacteristics);
            _statsController.UpHealth();
        }
    }

    public void RecoveryHealthButton()
    {
        if (_wallet.WalletСontent >= PriceForRecoverHealth)
        {
            _statsController.HealthRecovery();
            _wallet.RemoveFromScore(PriceForRecoverHealth);
        }
    }

    public void PressStartButton()
    {
        _playerMovement.StartMovement();
        _gameStatesController.GameStartedState(true);
        _startButton.SetActive(false);
    }
}