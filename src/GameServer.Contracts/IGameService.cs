using System;
using System.ServiceModel;
using Core.Models.Heroes;
using Core.Models.Items;
using Core.Models.Units;
using Interfaces;

namespace GameServer.Contracts
{
    [ServiceContract(CallbackContract = typeof(IGameServiceCallback), SessionMode = SessionMode.Required)]
    [ServiceKnownType(typeof(CMage))]
    [ServiceKnownType(typeof(CPaladin))]
    [ServiceKnownType(typeof(CThief))]
    [ServiceKnownType(typeof(CWarrior))]
    [ServiceKnownType(typeof(CHeroBase))]
    [ServiceKnownType(typeof(CMonster))]
    [ServiceKnownType(typeof(CXpBook))]
    [ServiceKnownType(typeof(CHealthPotion))]
    [ServiceKnownType(typeof(CSingleUseItem))]
    [ServiceKnownType(typeof(CWeapon))]
    [ServiceKnownType(typeof(CItemBase))]
    public interface IGameService
    {
        [OperationContract]
        String Connect(Guid playerId);

        [OperationContract]
        void FinishRound(String session);

        [OperationContract(IsOneWay = true)]
        void SelectHero(CHeroBase hero);

        [OperationContract]
        Boolean TryMoveUnit(SPoint cellPosition, SPoint newPosition);

        [OperationContract(IsOneWay = true)]
        void KillUnit(SPoint position);
    }
}