using System;

namespace Code.Services.Wallet
{
    public interface IWalletService
    {
        int Coins { get; }
        event Action<int> CoinsChanged;
        void Earn(int amount);
        bool TrySpend(int amount);
        bool CanAfford(int price);
    }
}