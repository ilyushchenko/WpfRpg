using System;
using Core.Data;
using GameServerWCF.Interfaces.GameChoiceService;

namespace GameServerWCF
{
    public class CGamePlayer
    {
        public CGamePlayer(CPlayer playerInfo)
        {
            PlayerInfo = playerInfo;
            Status = EPlayerStatus.NotPlaying;
        }

        public CPlayer PlayerInfo { get; }
        public CGame Game { get; set; }
        public EPlayerStatus Status { get; set; }

        public IGameChoiceServiceCallback Callback { get; set; }

        /// <summary>
        ///     Last player activity time.
        /// </summary>
        public DateTime LastActivity { get; private set; }
    }
}