using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private Button _upDamageButton;
    [SerializeField] private Button _upHealthButton;
    [SerializeField] private Button _upSpeedAttackButton;
    [SerializeField] private Button _recoveryHealthButton;
    [SerializeField] private Player _player;

    private readonly Market _market = new Market();
    
    
    private void Awake()
    {
        _upDamageButton.interactable = false;
        _upHealthButton.interactable = false;
        _upSpeedAttackButton.interactable = false;
        _recoveryHealthButton.interactable = false;
    }

    protected void Start()
    {
        _player.Wallet.Changed += OnChangeWallet;
    }

    protected void OnDestroy()
    {
        _player.Wallet.Changed -= OnChangeWallet;
    }

    private void OnChangeWallet(int currentWalletValue)
    {
        if (currentWalletValue >= _market.PriceForCharacteristics)
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

        if (currentWalletValue >= _market.PriceForRecoverHealth)
        {
            _recoveryHealthButton.interactable = true;
        }
        else
        {
            _recoveryHealthButton.interactable = false;
        }
    }
    
    public void UpDamageButton()
    {
        _market.TryBuyUpgrade(_player.Wallet);
        _player.StatsController.UpDamage();
    }

    public void UpSpeedAttackButton()
    {
        _market.TryBuyUpgrade(_player.Wallet);
        _player.StatsController.UpSpeedAttack();
    }
    
    public void UpHealthButton()
    {
        _market.TryBuyUpgrade(_player.Wallet);
        _player.StatsController.UpHealth();
    }
    
    public void RecoveryHealthButton()
    {
        _market.TryBuyRecoveryHealth(_player.Wallet);
        _player.StatsController.HealthRecovery();
    }
    
    public void RestartButton()
    {
        _sceneLoader.LoadMainScene();
    }
    
    public void PressStartButton()
    {
        _playerMovement.StartMovement();
        _startButton.SetActive(false);
    }
}