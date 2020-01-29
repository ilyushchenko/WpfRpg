using Core.Data;
using Core.Models.Heroes;
using GameServer.Contracts;

namespace GameServerWCF
{
    public class CServerPlayer
    {
        public CPlayer PlayerInfo { get; set; }
        public IGameServiceCallback Callback { get; set; }
        public CHeroBase Hero { get; set; }
        public EGameStatus Status { get; set; }
        public string Session { get; set; }
    }
}