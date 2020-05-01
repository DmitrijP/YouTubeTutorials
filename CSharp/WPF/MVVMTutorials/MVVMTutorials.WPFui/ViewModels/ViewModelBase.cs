using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMTutorials.WPFui.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public virtual void Intitialize(object initialize){}
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Changed([CallerMemberName]string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
