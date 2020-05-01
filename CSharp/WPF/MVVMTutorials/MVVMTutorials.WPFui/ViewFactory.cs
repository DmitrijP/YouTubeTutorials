namespace MVVMTutorials.WPFui
{
    public class ViewFactory
    {
        public void OpenWindow<T>(T viewModel)
        {
            var myType = typeof(T);
            var myAssembly = myType.Assembly;
            //SecondaryViewModel
            var viewModelName = myType.Name;
            //MVVMTutorials.WPFui.ViewModels
            var viewModelNamespace = myType.Namespace;
            //ViewNamen generieren SecondaryWindow
            var viewName = viewModelName.Replace("ViewModel", "Window");
            //ViewNamespace generieren MVVMTutorials.WPFui.Views
            var viewNamespace = viewModelNamespace.Replace("ViewModels", "Views");
            //View FullName generieren  MVVMTutorials.WPFui.Views.SecondaryWindow
            var viewFullName = $"{viewNamespace}.{viewName}";
            //View Instance erstellen
            var myInstance = myAssembly.CreateInstance(viewFullName);

            //DataContext Property suchen
            var propertyInfo = myInstance.GetType().GetProperty("DataContext");
            //DataContext Property belegen
            propertyInfo.SetValue(myInstance, viewModel);

            //Show Methode suchen
            var methodInfo = myInstance.GetType().GetMethod("Show");
            //Show Methode aufrufen
            methodInfo.Invoke(myInstance, null);
        }

        public void OpenWindow<T>()
        {
            var vm = App.Container.Resolve<T>();
            App.ViewFactory.OpenWindow<T>(vm);
        }
    }
}
