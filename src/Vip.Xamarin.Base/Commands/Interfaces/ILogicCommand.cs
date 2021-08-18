namespace Vip.Xamarin.Base
{
    public interface ILogicCommand<in TIn, out TOut>
    {
        TOut Execute(TIn request);
    }
}