using System;
using System.ServiceModel;
using Core.Data;

namespace GameServerWCF.Interfaces.GameChoiceService
{
    [ServiceContract]
    public interface IGameChoiceServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void PlayerHasConnected(CPlayer player);

        [OperationContract(IsOneWay = true)]
        void PlayerHasDisconnected(CPlayer player);

        [OperationContract(IsOneWay = true)]
        void GameHasStarted(String gameUri);
    }
}