using System;

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
        Changed?.Invoke(Coins);
    }
}