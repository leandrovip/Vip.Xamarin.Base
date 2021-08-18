using System.Threading.Tasks;

namespace Vip.Xamarin.Base
{
    public interface IAsyncLogicCommand<TIn, TOut> : ILogicCommand<TIn, TOut> where TOut : CommandResult
    {
        Task<TOut> ExecuteAsync(TIn request);
    }
}