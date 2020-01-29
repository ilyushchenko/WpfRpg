using System;
using System.ServiceModel;
using Core.Models;
using Core.Models.Heroes;
using Core.Models.Items;
using Core.Models.Terrains;
using Core.Models.Units;
using Interfaces;

namespace GameServer.Contracts
{
    [ServiceContract]
    [ServiceKnownType(typeof(CMap))]
    [ServiceKnownType(typeof(CCell))]
    [ServiceKnownType(typeof(CGrass))]
    [ServiceKnownType(typeof(CMage))]
    [ServiceKnownType(typeof(CPaladin))]
    [ServiceKnownType(typeof(CThief))]
    [ServiceKnownType(typeof(CWarrior))]
    [ServiceKnownType(typeof(CHeroBase))]
    [ServiceKnownType(typeof(CMonster))]
    [ServiceKnownType(typeof(CWall))]
    [ServiceKnownType(typeof(CDirt))]
    [ServiceKnownType(typeof(CTrader))]
    [ServiceKnownType(typeof(CXpBook))]
    [ServiceKnownType(typeof(CHealthPotion))]
    [ServiceKnownType(typeof(CSingleUseItem))]
    [ServiceKnownType(typeof(CWeapon))]
    public interface IGameServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void GameHasStarted(CMap map, CHeroBase hero);

        [OperationContract(IsOneWay = true)]
        void RoundHasStarted(Int32 roundNumber, TimeSpan roundTime);

        [OperationContract(IsOneWay = true)]
        void RoundHasEnded(TimeSpan timeout);

        [OperationContract(IsOneWay = true)]
        void GameHasEnded();

        [OperationContract(IsOneWay = true)]
        void HeroHasMoved(CHeroBase hero, SPoint oldPosition, SPoint newPosition);
        [OperationContract(IsOneWay = true)]
        void UnitHasDied(SPoint position);
    }
}