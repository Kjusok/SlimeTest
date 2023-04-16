using System;

public class Wallet
{
    private int _coins;

    public int Coins
    {
        get => _coins;
        private set
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
    
    public void AddCoins(int coins)
    {
        Coins += coins;
    }

    public void BuyFromPrice(int coins)
    {
        Coins -= coins;
    }
}