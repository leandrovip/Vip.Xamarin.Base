namespace Vip.Xamarin.Base
{
    /// <summary>
    ///     Base class for which commands can inherit.
    ///     Created to enforce single responsibility design principal in application logic implementation
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public abstract class LogicCommand<TIn, TOut> : ILogicCommand<TIn, TOut> where TOut : CommandResult
    {
        public abstract TOut Execute(TIn request);
    }
}