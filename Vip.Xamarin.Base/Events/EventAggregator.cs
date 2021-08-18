using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Vip.Xamarin.Base
{
    public class EventAggregator : IEventAggregator
    {
        #region Propriedades

        private readonly List<Delegate> _handlersParameter;
        private readonly List<EventDelegates> _handlersEvent;
        private readonly SynchronizationContext _syncContext;

        #endregion

        #region Construtores

        public EventAggregator()
        {
            _handlersParameter = new List<Delegate>();
            _handlersEvent = new List<EventDelegates>();
            _syncContext = SynchronizationContext.Current;
        }

        #endregion

        #region Métodos COM Parametro

        public void SendHandler<T>(T handler) where T : IEventHandler
        {
            ProcessEventWithParameter(handler, false);
        }

        public void PostHandler<T>(T handler) where T : IEventHandler
        {
            ProcessEventWithParameter(handler, true);
        }

        public void RegisterHandler<T>(Action<T> actionHandler) where T : IEventHandler
        {
            if (actionHandler == null) throw new ArgumentNullException(nameof(actionHandler));
            if (_handlersParameter.Exists(x => x.Method == actionHandler.Method && x.Target.GetType() == actionHandler.Target.GetType())) return;

            _handlersParameter.Add(actionHandler);
        }

        public void UregisterHandler<T>(Action<T> actionHandler) where T : IEventHandler
        {
            if (actionHandler == null) throw new ArgumentNullException(nameof(actionHandler));
            _handlersParameter.Remove(actionHandler);
        }

        #endregion

        #region Métodos SEM Parametro

        public void SendHandler<T>() where T : IEventHandler
        {
            ProcessEvent<T>(false);
        }

        public void PostHandler<T>() where T : IEventHandler
        {
            ProcessEvent<T>(true);
        }

        public void RegisterHandler<T>(Action actionHandler) where T : IEventHandler
        {
            if (actionHandler == null) throw new ArgumentNullException(nameof(actionHandler));
            if (_handlersEvent.Exists(x => x.Action.Method == actionHandler.Method && x.Action.Target.GetType() == actionHandler.Target.GetType())) return;

            _handlersEvent.Add(new EventDelegates {Action = actionHandler, ActionType = typeof(T)});
        }

        public void UregisterHandler<T>(Action actionHandler) where T : IEventHandler
        {
            if (actionHandler == null) throw new ArgumentNullException(nameof(actionHandler));
            _handlersEvent.RemoveAll(x => x.ActionType == typeof(T) && x.Action.Equals(actionHandler));
        }

        #endregion

        #region Métodos Privados

        private void ProcessEventWithParameter<T>(T handler, bool isPost)
        {
            if (handler == null) return;

            if (_syncContext != null)
            {
                if (isPost)
                    _syncContext.Post(e => Dispatch((T) e), handler);
                else
                    _syncContext.Send(e => Dispatch((T) e), handler);
            }
            else
            {
                Dispatch(handler);
            }
        }

        private void ProcessEvent<T>(bool isPost)
        {
            if (_syncContext != null)
            {
                if (isPost)
                    _syncContext.Post(_ => Dispatch<T>(), null);
                else
                    _syncContext.Send(_ => Dispatch<T>(), null);
            }
            else
            {
                Dispatch<T>();
            }
        }

        private void Dispatch<T>(T message)
        {
            var compatibleHandlers = _handlersParameter.OfType<Action<T>>().ToList();
            foreach (var handler in compatibleHandlers) handler(message);
        }

        private void Dispatch<T>()
        {
            var compatibleHandlers = _handlersEvent.Where(x => x.ActionType == typeof(T)).ToList();
            foreach (var handler in compatibleHandlers) handler.Action?.Invoke();
        }

        #endregion

        #region Métodos Estáticos

        private static EventAggregator _instance;
        public static EventAggregator Instance => _instance ??= new EventAggregator();

        #endregion
    }
}