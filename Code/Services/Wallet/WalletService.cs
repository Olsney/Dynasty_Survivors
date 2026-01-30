using System;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;

namespace Code.Services.Wallet
{
    public class WalletService : IWalletService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        
        public int Coins => _progressService.Progress.WorldData.LootData.LootValue;
        
        public event Action<int> CoinsChanged;

        public WalletService(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Earn(int amount)
        {
            if (amount <= 0)
                return;

            _progressService.Progress.WorldData.LootData.Earn(amount);
            CoinsChanged?.Invoke(Coins);
            _saveLoadService.SaveProgress();
        }

        public bool TrySpend(int amount)
        {
            if (!CanAfford(amount))
                return false;

            _progressService.Progress.WorldData.LootData.Spend(amount) ;
            CoinsChanged?.Invoke(Coins);
            _saveLoadService.SaveProgress();
            
            return true;
        }

        public bool CanAfford(int price) => 
            Coins >= price;
    }
}