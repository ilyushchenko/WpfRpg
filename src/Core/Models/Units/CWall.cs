using System.Runtime.Serialization;
using Interfaces;

namespace Core.Models.Units
{
    [DataContract]
    public class CWall : IPositionable
    {
        [DataMember]
        private IMap _map;

        private CWall(EWallOrientation orientation)
        {
            Orientation = orientation;
        }

        [DataMember]
        public EWallOrientation Orientation { get; set; }

        [DataMember]
        public SPoint Position { get; private set; }

        public void SetMap(IMap map)
        {
            _map = map;
        }

        public void SetPosition(SPoint position)
        {
            Position = position;
            _map.SetUnit(this, position);
        }

        public static CWall Create(EWallOrientation orientation)
        {
            return new CWall(orientation);
        }
    }
}