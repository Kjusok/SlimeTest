public class Market
{
    public int PriceForCharacteristics { get; private set; } = 25;
    public int PriceForRecoverHealth { get; private set; } = 10;


    public void TryBuyUpgrade(Wallet wallet)
    {
        if (wallet.Coins >= PriceForCharacteristics)
        {
            wallet.BuyFromPrice(PriceForCharacteristics);
        }
    }

    public void TryBuyRecoveryHealth(Wallet wallet)
    {
        if (wallet.Coins >= PriceForRecoverHealth)
        {
            wallet.BuyFromPrice(PriceForRecoverHealth);
        }
    }
}