using System.Windows.Input;
using MVVMTutorials.WPFui.Commands;
using MVVMTutorials.WPFui.Entities;
using System.Collections.ObjectModel;
using MVVMTutorials.WPFui.DataStores;
using System.Collections.Generic;
using MVVMTutorials.WPFui.Models;
using System.Linq;

namespace MVVMTutorials.WPFui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEmployeeStore _employeeStore;
        private readonly IMessenger _messenger;

        public MainViewModel(IEmployeeStore employeeStore, IMessenger messenger)
        {
            InitializeCommands();
            _employeeStore = employeeStore ?? throw new System.ArgumentNullException(nameof(employeeStore));
            _messenger = messenger;
            var stringSearchOperators = new List<SearchOperatorModel>
            {
                new SearchOperatorModel
                {
                    Name = "",
                    Operator = SearchOperatorEnum.NoSelection
                },
                new SearchOperatorModel
                {
                    Name = "gleich",
                    Operator = SearchOperatorEnum.equals
                } ,
                new SearchOperatorModel
                {
                    Name = "ähnlich",
                    Operator =  SearchOperatorEnum.like
                },
                new SearchOperatorModel
                {
                    Name = "enthällt",
                    Operator =  SearchOperatorEnum.like
                }
            };
            var numberSearchOperators = new List<SearchOperatorModel>
            {
                new SearchOperatorModel
                {
                    Name = "",
                    Operator = SearchOperatorEnum.NoSelection
                },
                new SearchOperatorModel
                {
                    Name = "=",
                    Operator =  SearchOperatorEnum.equals
                } ,
                new SearchOperatorModel
                {
                    Name = "<",
                    Operator =  SearchOperatorEnum.smaller
                },
                new SearchOperatorModel
                {
                    Name = ">",
                    Operator =  SearchOperatorEnum.greater
                }
            };
            SearchFieldViewModels = new List<SearchFieldViewModel>
            {
                new SearchFieldViewModel
                {
                    SearchFieldName = "Vorname",
                    PossibleSearchOperators = stringSearchOperators
                },
                new SearchFieldViewModel
                {
                    SearchFieldName = "Nachname",
                    PossibleSearchOperators = stringSearchOperators
                }    ,
                new SearchFieldViewModel
                {
                    SearchFieldName = "Alter",
                    PossibleSearchOperators = numberSearchOperators
                },
                new SearchFieldViewModel
                {
                    SearchFieldName = "Angestellt als",
                    PossibleSearchOperators = stringSearchOperators
                },
                new SearchFieldViewModel
                {
                    SearchFieldName = "Benutzername",
                    PossibleSearchOperators = stringSearchOperators
                },
                new SearchFieldViewModel
                {
                    SearchFieldName = "Angestellt bei",
                    PossibleSearchOperators = stringSearchOperators
                },
                new SearchFieldViewModel
                {
                    SearchFieldName = "Stadt",
                    PossibleSearchOperators = stringSearchOperators
                }
            };
        }

        public override void Intitialize(object initialize)
        {
            base.Intitialize(initialize);
        }

        private string _mainTextBox;
        public string MainTextBox { get { return _mainTextBox; } set { _mainTextBox = value; Changed(); } }

        ObservableCollection<EmployeeEntity> _employeeCollection;
        public ObservableCollection<EmployeeEntity> EmployeeCollection
        {
            get { return _employeeCollection; }
            set { _employeeCollection = value; Changed(); }
        }

        public List<SearchFieldViewModel> SearchFieldViewModels { get; set; }

        private void InitializeCommands()
        {
            OpenSecondWindowCommand = new GenericCommand(
                () =>
                {
                    App.ViewFactory.OpenWindow<SecondaryViewModel>();
                });
            SendMessageToSecondWindowCommand = new GenericCommand(() => {
                _messenger.Send<EmployeeEntity>(_employeeCollection?.FirstOrDefault());
            });
            GetEmployeeListCommand = new GenericCommand(
                async () =>
                {
                    var employeeList = await _employeeStore.GetAll();
                    EmployeeCollection = new ObservableCollection<EmployeeEntity>(employeeList);
                });
        }

        public ICommand OpenSecondWindowCommand { get; set; }
        public ICommand SendMessageToSecondWindowCommand { get; set; }
        public ICommand GetEmployeeListCommand { get; set; }
    }
}
