using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using Interfaces;

namespace UI.Models.Heroes
{
    //public class CHeroBase_ : IHero, IMovable, IFightingUnit, IRoundUpdatable, ITradable, INotifyPropertyChanged
    //{
    //    private Int32 _healthPoints;
    //    private Int32 _level;
    //    private IMap _map;
    //    private Int32 _maxHealthPoints;
    //    private Int32 _maxMovingEnergy;
    //    private Int32 _movingEnergy;
    //    private Int32 _nextLevelXp;
    //    private Int32 _xp;

    //    public CHeroBase_()
    //    {
    //        MaxHealthPoints = 100;
    //        HealthPoints = 70;
    //        Xp = 30;
    //        NextLevelXp = 95;
    //    }

    //    public CHeroBase_(IMap map, String name, String pathToImage)
    //    {
    //        _map = map;
    //        Name = name;
    //        Image = new BitmapImage(new Uri(pathToImage, UriKind.Relative));
    //        Inventory = new List<IInventoryItem>();
    //    }

    //    public String Name { get; set; }

    //    public Int32 HealthPoints
    //    {
    //        get => _healthPoints;
    //        set
    //        {
    //            _healthPoints = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public Int32 MaxHealthPoints
    //    {
    //        get => _maxHealthPoints;
    //        set
    //        {
    //            _maxHealthPoints = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public BitmapImage Image { get; set; }
    //    public SPoint Position => Cell.Position;

    //    public Int32 Level
    //    {
    //        get => _level;
    //        set
    //        {
    //            _level = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public Int32 Xp
    //    {
    //        get => _xp;
    //        set
    //        {
    //            _xp = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public Int32 NextLevelXp
    //    {
    //        get => _nextLevelXp;
    //        set
    //        {
    //            _nextLevelXp = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public List<IInventoryItem> Inventory { get; set; }
    //    public Int32 Gold { get; set; }
    //    public Double MaxWeight { get; }
    //    public Double CurrentWeight => Inventory.Sum(i => i.Weight);

    //    public Int32 MovingEnergy
    //    {
    //        get => _movingEnergy;
    //        set
    //        {
    //            _movingEnergy = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public Int32 MaxMovingEnergy
    //    {
    //        get => _maxMovingEnergy;
    //        set
    //        {
    //            _maxMovingEnergy = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public virtual Boolean CanMove()
    //    {
    //        return CurrentWeight <= MaxWeight;
    //    }

    //    public void MoveTo(SPoint newPosition)
    //    {
    //        if (!_map.TransferUnit(this, newPosition)) return;
    //        SetPosition(newPosition);
    //    }

    //    public ICell Cell { get; private set; }

    //    public void SetMap(IMap map)
    //    {
    //        _map = map;
    //    }

    //    public void SetPosition(SPoint newPosition)
    //    {
    //        Cell = _map.SetUnit(this, newPosition);
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public void OnPropertyChanged([CallerMemberName] String prop = "")
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    //    }

    //    public virtual void ApplyDamage(IDamaging damagingItem)
    //    {
    //        HealthPoints -= damagingItem.Damage;
    //    }

    //    public void RoundStarted(Int32 roundNumber)
    //    {
    //        MovingEnergy = MaxMovingEnergy;
    //    }
    //}
}