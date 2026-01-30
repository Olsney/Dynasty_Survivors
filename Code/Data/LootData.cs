using System;

namespace Code.Data
{
    [Serializable]
    public class LootData
    {
        public int LootValue = 500;
        public Action Changed;

        public void Collect(Loot loot)
        {
            LootValue += loot.Value;
            Changed?.Invoke();
        }

        public void Earn(int amount)
        {
            if (amount <= 0)
                return;
            
            LootValue += amount;
            Changed?.Invoke();
        }

        public void Spend(int amount)
        {
            if (amount <= 0)
                return;

            if (LootValue - amount < 0)
            {
                LootValue = 0;
                Changed?.Invoke();
            }

            LootValue -= amount;
            Changed?.Invoke();
        }
    }
}