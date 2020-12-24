using MVVMTutorials.WPFui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MVVMTutorials.WPFui
{
    //Messanger für MVVM
    //Voraussetzungen sind:
    //Fortgeschrittene: Klassen, Interfaces, MVVM/MVC, Reflection, Generics List<T>
    //Kommunikation zwischen den View Models haben

    public class VM1
    {
        private readonly IMessenger messanger;

        public VM1(IMessenger messanger)
        {
            this.messanger = messanger;
        }

        public void SendMessageToVM2()
        {
            messanger.Send<string>("hallo");
        }

        public void SendMessageToVM3()
        {
            messanger.Send(1);
        }
    }


    public class VM3 : IHandle<string>, IHandle<int>
    {
        private readonly IMessenger messanger;

        public VM3(IMessenger messanger)
        {
            this.messanger = messanger;
            this.messanger.Subscribe(this);
        }

        public void Handle(string message)
        {
        }

        public void Handle(int message)
        {
        }

        public void CleanUp()
        {
            this.messanger.UnSubscribe(this);
        }
    }


    public class VM2 : IHandle<int>
    {
        private readonly IMessenger messanger;
        public VM2(IMessenger messanger)
        {
            this.messanger = messanger;
        }

        public void StartHandlingMessanges()
        {
            this.messanger.Subscribe(this);
        }

        public void StopHandlingMessanges()
        {
            this.messanger.UnSubscribe(this);
        }

        public void Handle(int message)
        {
        }
    }

    public interface IHandle<T>
    {
        void Handle(T message);
    }

    public interface IMessenger
    {
        void Subscribe(object vm);
        void UnSubscribe(object vm);
        void Send<T>(T message);
    }

    public class Messenger : IMessenger
    {
        private readonly List<Subscriber> _subscriber = new List<Subscriber>();

        public void Send<T>(T message)
        {
            foreach (var s in _subscriber)
                s.Handle(message);
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
            private object _vm;
            Dictionary<Type, MethodInfo> _messageHandlers = new Dictionary<Type, MethodInfo>();

            public Subscriber(object vm)
            {
                _vm = vm;
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

            public void Handle(object message)
            {
                var type = message.GetType();
                if (_messageHandlers.ContainsKey(type))
                {
                    _messageHandlers[type].Invoke(_vm, new[] { message });
                }
            }
        }
    
    }


}
