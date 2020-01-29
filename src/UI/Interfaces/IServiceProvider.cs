namespace UI.Interfaces
{
    internal interface IServiceProvider<out T>
    {
        T Service { get; }
    }

    public interface IServiceProvider<out TService, out TCallback>
    {
        TService Service { get; }
        TCallback Callback { get; }
    }
}
