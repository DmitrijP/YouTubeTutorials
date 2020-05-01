using System.Windows.Input;
using System.Collections.Generic;
using MVVMTutorials.WPFui.Commands;

namespace MVVMTutorials.WPFui.ViewModels
{
    public class SearchFieldViewModel : ViewModelBase
    {
        public SearchFieldViewModel()
        {
            ClearSearchCommand = new GenericCommand(
                () => { IsInitial = true; SearchValue = string.Empty; SelectedSearchOperator = null; });
        }

        private bool _isInitial;
        public bool IsInitial { get { return _isInitial; } set { _isInitial = value; Changed(); } }
        public string SearchFieldName { get; set; }

        string _searchValue;
        public string SearchValue { get { return _searchValue; } set { _searchValue = value; Changed(); } }
        public List<SearchOperatorModel> PossibleSearchOperators { get; set; }
        
        SearchOperatorModel _selectedSearchOperator;
        public SearchOperatorModel SelectedSearchOperator { get { return _selectedSearchOperator; } set {  _selectedSearchOperator = value; Changed(); } }
        public ICommand ClearSearchCommand { get; set; }
    }

    public class SearchOperatorModel
    {
        public string Name { get; set; }
        public SearchOperatorEnum Operator { get; set; }
    }

    public enum SearchOperatorEnum
    { 
        equals,
        greater,
        smaller,
        like,
        between,
        contains,
        NoSelection
    }
}
