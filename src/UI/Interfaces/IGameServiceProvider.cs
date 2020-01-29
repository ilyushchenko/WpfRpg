using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Contracts;
using UI.GameServer;
using UI.Services;

namespace UI.Interfaces
{
    public interface IGameServiceProvider
    {
        /// <summary>
        /// <see cref="IGameService"/> client.
        /// </summary>
        IGameService GameClient { get; } 

        /// <summary>
        /// <see cref="IGameService"/> callback.
        /// </summary>
        СGameServiceCallback ServiceCallback { get; }
    }
}
