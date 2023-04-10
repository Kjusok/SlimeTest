using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 1) Активация кнопок в игре
/// 2) увеличивать различные характериски игрока после нажатия кнопки (повышение жизни, пополнение здоровья, увилечение урона, увелечение скорости атаки)
/// </summary>
public class UIController : MonoBehaviour
{
    private const int PriceForCharacteristics = 25;
    private const int PriceForRecoverHealth = 10;

    [SerializeField] private SceneController _sceneController;
    [SerializeField] private GameStatesController _gameStatesController;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private Button _upDamageButton;
    [SerializeField] private Button _upHealthButton;
    [SerializeField] private Button _upSpeedAttackButton;
    [SerializeField] private Button _recoveryHealthButton;
    [SerializeField] private Player _player;
    
   
    private void Awake()
    {
        _upDamageButton.interactable = false;
        _upHealthButton.interactable = false;
        _upSpeedAttackButton.interactable = false;
        _recoveryHealthButton.interactable = false;
    }

    protected void OnEnable()
    {
        _wallet.Changed += OnChangeWallet;
    }

    protected void OnDisable()
    {
        _wallet.Changed -= OnChangeWallet;
    }

    private void OnChangeWallet(int currentWalletValue)
    {
        if (currentWalletValue >= PriceForCharacteristics)
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

        if (currentWalletValue >= PriceForRecoverHealth)
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
        if (_wallet.Coins >= PriceForCharacteristics)
        {
            _player.StatsController.UpDamage();
            _wallet.Coins -= PriceForCharacteristics;
        }
    }

    public void UpSpeedAttackButton()
    {
        if (_wallet.Coins >= PriceForCharacteristics)
        {
            _player.StatsController.UpSpeedAttack();
            _wallet.Coins -= PriceForCharacteristics;
        }
    }

    public void UpHealthButton()
    {
        if (_wallet.Coins >= PriceForCharacteristics)
        {
            _wallet.Coins -= PriceForCharacteristics;
            _player.StatsController.UpHealth();
        }
    }

    public void RecoveryHealthButton()
    {
        if (_wallet.Coins >= PriceForRecoverHealth)
        {
            _player.StatsController.HealthRecovery();
            _wallet.Coins -= PriceForRecoverHealth;
        }
    }

    public void PressStartButton()
    {
        _playerMovement.StartMovement();
        _gameStatesController.GameStart();
        _startButton.SetActive(false);
    }
}