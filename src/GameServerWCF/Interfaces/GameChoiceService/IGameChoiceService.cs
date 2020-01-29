using System;
using System.ServiceModel;
using Core.Data;

namespace GameServerWCF.Interfaces.GameChoiceService
{
    [ServiceContract(CallbackContract = typeof(IGameChoiceServiceCallback))]
    public interface IGameChoiceService
    {
        [OperationContract]
        Boolean LogIn(String login, Guid id, out CPlayer player);

        [OperationContract]
        CGameInfo[] GetGames();

        [OperationContract]
        CGameInfo CreateGame(String name, Int32 maxPlayers);

        [OperationContract]
        Boolean TryConnect(Guid gameId, out CGameInfo gameInfo);

        [OperationContract(IsOneWay = true)]
        void Ready(Guid gameId);

        [OperationContract(IsOneWay = true)]
        void Disconnect(Guid gameId);
    }
}