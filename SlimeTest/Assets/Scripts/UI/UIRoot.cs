using System;
using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    private const int PriceForCharacteristics = 25;
    private const int PriceForRecoverHealth = 10;

    [SerializeField] private SceneLoader _sceneLoader;
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
    
    private void TryBuyUpgrade(int price, Action upgradeHandler)
    {
        if (_player.Wallet.Coins >= price)
        {
            _player.Wallet.Coins -= price;
            upgradeHandler();
        }
    }
    
    public void RestartButton()
    {
        _sceneLoader.LoadMainScene();
    }
    
    public void UpDamageButton()
    {
        TryBuyUpgrade(PriceForCharacteristics, _player.StatsController.UpDamage);
    }

    public void UpSpeedAttackButton()
    {
        TryBuyUpgrade(PriceForCharacteristics, _player.StatsController.UpSpeedAttack);
    }

    public void UpHealthButton()
    {
        TryBuyUpgrade(PriceForCharacteristics, _player.StatsController.UpHealth);
    }

    public void RecoveryHealthButton()
    {
        TryBuyUpgrade(PriceForRecoverHealth, _player.StatsController.HealthRecovery);
    }

    public void PressStartButton()
    {
        _playerMovement.StartMovement();
        _startButton.SetActive(false);
    }
}