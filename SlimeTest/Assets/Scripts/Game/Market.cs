public class Market
{
    public readonly int PriceForCharacteristics = 25;
    public readonly int PriceForRecoverHealth = 10;


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