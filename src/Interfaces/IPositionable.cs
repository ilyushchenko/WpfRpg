namespace Interfaces
{
    public interface IPositionable
    {
        SPoint Position { get; }
        void SetMap(IMap map);
        void SetPosition(SPoint position);
    }
}