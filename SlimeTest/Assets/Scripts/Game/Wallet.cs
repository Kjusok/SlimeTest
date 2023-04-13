using System;using System.Runtime.InteropServices;

/// 1) Добовлять и убирать очки (монетки) из кошелька

public class Wallet
{
    private int _coins;

    public int Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            ChangeScore();
        }
    }
    public event Action<int> Changed;

    
    private void ChangeScore()
    {
        //_walletText.text = string.Format("{0:D6}", Coins);
        
        Changed?.Invoke(Coins);
    }
}