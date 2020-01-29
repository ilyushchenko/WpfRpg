using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Interfaces;

namespace Core.Models.Units
{
    [DataContract]
    public class CTrader : ITrader, IPositionable
    {
        [DataMember]
        private readonly List<IInventoryItem> _inventory;
        [DataMember]
        private readonly Dictionary<ITradable, Double> _tradeCoefficients;

        public CTrader() : this(new List<IInventoryItem>())
        {

        }

        public CTrader(List<IInventoryItem> items)
        {
            _inventory = items;
            _tradeCoefficients = new Dictionary<ITradable, Double>();
            Gold = 100000;
        }

        [DataMember]
        public IMap Map { get; private set; }

        [DataMember]
        public SPoint Position { get; private set; }

        [DataMember]
        public Int32 Gold { get; set; }
        public IReadOnlyList<IInventoryItem> Inventory => _inventory.AsReadOnly();

        public void SetPosition(SPoint position)
        {
            Position = position;
            Map.SetUnit(this, position);
        }

        public void SetMap(IMap map)
        {
            Map = map;
        }

        public Boolean BuyItem(ITradable seller, IInventoryItem itemToTrade)
        {
            Double tradeCoefficient = GetTradeCoefficient(seller);

            var finalCost = (Int32) (itemToTrade.Cost * tradeCoefficient);

            if (Gold >= finalCost)
            {
                _inventory.Add(itemToTrade);
                Gold -= finalCost;

                seller.Gold += finalCost;
                seller.Inventory.Remove(itemToTrade);

                return true;
            }

            return false;
        }

        public Boolean SellItem(ITradable buyer, IInventoryItem itemToTrade)
        {
            //TODO: Calculate gold
            Double tradeCoefficient = GetTradeCoefficient(buyer);
            Int32 finalCost = itemToTrade.Cost + (Int32) (itemToTrade.Cost * (1 - tradeCoefficient));

            if (buyer.Gold >= finalCost)
            {
                _inventory.Remove(itemToTrade);
                Gold += finalCost;

                buyer.Gold -= finalCost;
                buyer.Inventory.Add(itemToTrade);

                return true;
            }

            return false;
        }

        public Double GetTradeCoefficient(ITradable hero)
        {
            if (!_tradeCoefficients.ContainsKey(hero))
            {
                //TODO: Move to constants
                var defaultTradeCoefficient = 0.8;
                _tradeCoefficients.Add(hero, defaultTradeCoefficient);
            }

            return _tradeCoefficients[hero];
        }
    }
}