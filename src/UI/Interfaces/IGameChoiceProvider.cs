using UI.GameServer;
using UI.Services;

namespace UI.Interfaces
{
    public interface IGameChoiceProvider : IServiceProvider<GameChoiceServiceClient, CGameChoiceServiceCallback>
    {
    }
}
