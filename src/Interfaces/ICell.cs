namespace Interfaces
{
    public interface ICell
    {
        SPoint Location { get; }
        ITerrain Terrain { get; set; }
        IPositionable Unit { get; set; }
    }
}