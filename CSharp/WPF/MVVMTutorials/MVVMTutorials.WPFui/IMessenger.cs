using MVVMTutorials.WPFui.Models;
using System.Text;

namespace MVVMTutorials.WPFui
{
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
}
