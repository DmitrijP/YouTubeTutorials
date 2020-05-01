using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMTutorials.WPFui.Models
{
    class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Changed([CallerMemberName]string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
