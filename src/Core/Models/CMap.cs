using System;
using System.Runtime.Serialization;
using Interfaces;

namespace Core.Models
{
    [DataContract(IsReference = true)]
    public class CMap : IMap
    {
        [DataMember] private readonly ICell[] _field;

        private CMap(Int32 width, Int32 height)
        {
            Width = width;
            Height = height;
            _field = new ICell[width * height];
        }

        /// <summary>
        ///     Grid width.
        /// </summary>
        [DataMember]
        public Int32 Width { get; set; }

        /// <summary>
        ///     Grid height.
        /// </summary>
        [DataMember]
        public Int32 Height { get; set; }

        public ICell GetCell(SPoint position)
        {
            Int32 index = GetIndex(position);
            return _field[index];
        }

        public event EventHandler MapUpdated;

        public Boolean SetUnit(IPositionable unit, SPoint point)
        {
            //TODO: Check cell on free space
            ICell cell = GetCell(point);
            cell.Unit = unit;
            MapUpdated?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void RemoveUnit(SPoint position)
        {
            ICell unitCell = GetCell(position);
            unitCell.Unit = null;
            MapUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void SetCell(ICell cell)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));
            Int32 index = GetIndex(cell.Location);
            _field[index] = cell;
        }

        private Int32 GetIndex(SPoint position)
        {
            return Width * position.Y + position.X;
        }

        public static CMap Create(IMapLoader loader)
        {
            Int32 width = loader.GetWidth();
            Int32 height = loader.GetHeight();

            var map = new CMap(width, height);

            for (var x = 0; x < map.Width; x++)
            for (var y = 0; y < map.Height; y++)
            {
                var position = new SPoint(x, y);
                ICell cell = loader.GetCell(position);
                map.SetCell(cell);

                IPositionable unit = loader.GetUnit(position);
                if (unit != null)
                {
                    unit.SetMap(map);
                    unit.SetPosition(new SPoint(x, y));
                    //map.Spawn(unit, position.X, position.Y);
                }
            }

            return map;
        }

        public Int32 GetPenalty(SPoint position)
        {
            ICell cell = GetCell(position);
            return cell.Terrain.MovementPenalty;
        }

        public void Move(IMovable unit, SPoint oldPosition, SPoint newPosition)
        {
            ICell oldCell = GetCell(oldPosition);
            ICell newCell = GetCell(newPosition);
            Int32 transferEnergy = GetPenalty(oldPosition);

            oldCell.Unit = null;
            newCell.Unit = unit;
            unit.MovingEnergy -= transferEnergy;
            unit.SetPosition(newPosition);
            MapUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}