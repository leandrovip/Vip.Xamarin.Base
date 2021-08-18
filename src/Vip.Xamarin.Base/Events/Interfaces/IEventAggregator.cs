using System;

namespace Vip.Xamarin.Base
{
    public interface IEventAggregator
    {
        void SendHandler<T>(T handler) where T : IEventHandler;
        void SendHandler<T>() where T : IEventHandler;
        void PostHandler<T>(T handler) where T : IEventHandler;
        void PostHandler<T>() where T : IEventHandler;
        void RegisterHandler<T>(Action<T> actionHandler) where T : IEventHandler;
        void RegisterHandler<T>(Action actionHandler) where T : IEventHandler;
        void UregisterHandler<T>(Action<T> actionHandler) where T : IEventHandler;
        void UregisterHandler<T>(Action actionHandler) where T : IEventHandler;
    }
}