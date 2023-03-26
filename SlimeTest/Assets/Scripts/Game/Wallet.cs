using UnityEngine;
using UnityEngine.UI;
/// 1) Добовлять и убирать очки (монетки) из кошелька

public class Wallet : MonoBehaviour
{
    [SerializeField] private Text _walletText;
    
    public int WalletСontent{ get; private set; }

    
    public void AddCoinsToWallet(int coins)
    {
        WalletСontent += coins;
        string scoreTextWithZero = string.Format("{0:D6}", WalletСontent);

        _walletText.text = scoreTextWithZero;
    }
    
    public void RemoveFromScore(int coins)
    {
        WalletСontent -= coins;
        string scoreTextWithZero = string.Format("{0:D6}", WalletСontent);

        _walletText.text = scoreTextWithZero;
    }
}