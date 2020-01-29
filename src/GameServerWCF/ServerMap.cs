using System;
using Core.Models;
using Core.Models.Heroes;
using Interfaces;

namespace GameServerWCF
{
    internal class CServerMap
    {
        private readonly CMap _map;
        private readonly CMapLockContainer _lockContainer;

        public CServerMap(CMap map)
        {
            _map = map;
            _lockContainer = new CMapLockContainer(map.Width, map.Height);
        }

        public CMap GetMap()
        {
            return _map;
        }

        public Boolean Spawn(IPositionable unit, Int32 x, Int32 y)
        {
            if (unit is null) throw new ArgumentNullException(nameof(unit));
            unit.SetMap(_map);
            var position = new SPoint(x, y);
            Boolean cellLocked = _lockContainer.TryLock(position);
            if (cellLocked)
            {
                try
                {
                    unit.SetPosition(position);
                }
                finally
                {
                    _lockContainer.Unlock(position);
                }
            }

            return cellLocked;
        }

        private Boolean TransferUnit(IMovable movableUnit, SPoint newPosition)
        {
            ICell destinationCell = _map.GetCell(newPosition);
            if (destinationCell.Unit != null) return false;

            Int32 transferEnergy = destinationCell.Terrain?.MovementPenalty ?? 0;
            if (transferEnergy > movableUnit.MovingEnergy) return false;

            SPoint oldPosition = movableUnit.Position;

            _map.Move(movableUnit, oldPosition, newPosition);

            return true;
        }

        public Boolean TryMove(SPoint oldPosition, SPoint newPosition, out CHeroBase hero)
        {
            hero = null;
            Boolean oldCellLocked = _lockContainer.TryLock(oldPosition);
            if (!oldCellLocked) return false;
            Boolean newCellLocked = _lockContainer.TryLock(newPosition);
            if (!newCellLocked)
            {
                _lockContainer.Unlock(oldPosition);
                return false;
            }

            try
            {
                ICell oldCell = _map.GetCell(oldPosition);

                hero = oldCell.Unit as CHeroBase;

                return TransferUnit(hero, newPosition);
            }
            finally
            {
                _lockContainer.Unlock(oldPosition);
                _lockContainer.Unlock(newPosition);
            }
        }
    }
}