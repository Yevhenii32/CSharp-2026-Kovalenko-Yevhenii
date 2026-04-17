using CommunityToolkit.Mvvm.ComponentModel;

namespace ProductManager.UI.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;
    }
}