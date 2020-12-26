using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MVVMTutorials.WPFui
{
    public class Messenger : IMessenger
    {
        private List<Subscriber> _subscriber = new List<Subscriber>();

        public void Send<T>(T message)
        {
            foreach (var s in _subscriber)
                s.Handle(message);
            var subscribers = from x in _subscriber where x.IsAlive select x;
            _subscriber = subscribers.ToList();
        }

        public void Subscribe(object vm)
        {
            var s = new Subscriber(vm);
            _subscriber.Add(s);
        }

        public void UnSubscribe(object vm)
        {
            var s = new Subscriber(vm);
            _subscriber.Remove(s);
        }
        
        public class Subscriber
        {
            private WeakReference _weakReferenceToVM;
            Dictionary<Type, MethodInfo> _messageHandlers = new Dictionary<Type, MethodInfo>();

            public Subscriber(object vm)
            {
                _weakReferenceToVM = new WeakReference(vm);
                var implementedInterfaces = vm.GetType()
                    .GetTypeInfo()
                    .ImplementedInterfaces;
                var iHandleInterfaces = implementedInterfaces
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandle<>));
                foreach (var @interface in iHandleInterfaces)
                {
                    var dataType = @interface
                        .GetTypeInfo()
                        .GenericTypeArguments[0];
                    var methodToExecute = @interface
                        .GetTypeInfo()
                        .GetRuntimeMethod("Handle", new [] { dataType});
                    if (methodToExecute != null)
                        _messageHandlers.Add(dataType, methodToExecute);
                }
            }

            public bool IsAlive => _weakReferenceToVM.IsAlive;

            public void Handle(object message)
            {
                if (message == null)
                    return;
                if (!_weakReferenceToVM.IsAlive)
                    return;
                var type = message.GetType();
                if (_messageHandlers.ContainsKey(type))
                {
                    _messageHandlers[type].Invoke(_weakReferenceToVM.Target, new[] { message });
                }
            }
        }
    
    }


}
