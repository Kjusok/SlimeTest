using UnityEngine;
using UnityEngine.UI;

public class UIWallet : MonoBehaviour
{
    [SerializeField] private Text _walletText;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.Wallet.Changed += ChangedScoreHandler;
    }

    private void OnDestroy()
    {
        _player.Wallet.Changed -= ChangedScoreHandler;
    }

    private void ChangedScoreHandler(int coins)
    {
        _walletText.text = string.Format("{0:D6}", coins);
    }
}