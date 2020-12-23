using MVVMTutorials.WPFui.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMTutorials.WPFui
{
    //Messanger für MVVM
    //Voraussetzungen sind:
    //Fortgeschrittene: Klassen, Interfaces, MVVM/MVC, Reflection, Generics List<T>
    //Kommunikation zwischen den View Models haben

    class VM1
    {
        private readonly IMessanger messanger;

        public VM1(IMessanger messanger)
        {
            this.messanger = messanger;
        }

        void SendMessageToVM2()
        {
            messanger.Send<string>("hallo");
        }

        void SendMessageToVM3()
        {
            messanger.Send(1);
        }
    }


    class VM3 : IHandle<string>, IHandle<int>, IHandle<EmployeeModel>
    {
        private readonly IMessanger messanger;

        public VM3(IMessanger messanger)
        {
            this.messanger = messanger;
            this.messanger.Subscribe(this);
        }

        public void Handle(string message)
        {
            throw new NotImplementedException();
        }

        public void Handle(int message)
        {
            throw new NotImplementedException();
        }

        public void Handle(EmployeeModel message)
        {
            throw new NotImplementedException();
        }

        public void CleanUp()
        {
            this.messanger.UnSubscribe(this);
        }
    }


    class VM2 : IHandle<int>
    {
        private readonly IMessanger messanger;
        public VM2(IMessanger messanger)
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
            throw new NotImplementedException();
        }
    }

    class VM4 : IHandle<EmployeeModel>
    {
        private readonly IMessanger messanger;
        public VM4(IMessanger messanger)
        {
            this.messanger = messanger;
            this.messanger.Subscribe(this);
        }

        public void Handle(EmployeeModel message)
        {
            throw new NotImplementedException();
        }
    }

    interface IHandle<T>
    {
        void Handle(T message);
    }

    interface IMessanger
    {
        void Subscribe(object vm);
        void UnSubscribe(object vm);
        void Send<T>(T message);
    }
}
