using System.Windows.Input;
using MVVMTutorials.WPFui.Commands;
using MVVMTutorials.WPFui.Entities;

namespace MVVMTutorials.WPFui.ViewModels
{
    public class SecondaryViewModel : ViewModelBase, IHandle<EmployeeEntity>
    {
        public SecondaryViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            _messenger.Subscribe(this);
            InitializeCommands();
        }

        private string _mainTextBox;
        private readonly IMessenger _messenger;

        public string MainTextBox { get { return _mainTextBox; } set { _mainTextBox = value; Changed(); } }

        private void InitializeCommands()
        {
            SendMessageToMainWindowCommand = new GenericCommand(
                () => { });
        }

        public void Handle(EmployeeEntity message)
        {
            if (message != null)
                MainTextBox = $"Mitarbeiter empfangen: {message.FirstName} {message.LastName}";
        }

        public ICommand SendMessageToMainWindowCommand { get; set; }
    }
}
