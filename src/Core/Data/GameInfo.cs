using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    [DataContract]
    public class CGameInfo
    {
        public CGameInfo()
        {
            Id = Guid.NewGuid();
            Players = new List<CPlayer>();
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public CMapInfo Map { get; set; }

        [DataMember]
        public int CurrentPlayersCount { get; set; }

        [DataMember]
        public Int32 MaxPlayers { get; set; }

        [DataMember]
        public List<CPlayer> Players { get; set; }
    }
}
