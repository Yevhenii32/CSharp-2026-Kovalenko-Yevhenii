using ProductManager.ViewModels;

namespace ProductManager.UI.Pages;

[QueryProperty(nameof(CurrentWarehouse), "SelectedWarehouse")]
public partial class WarehouseDetailsPage : ContentPage
{
    private WarehouseViewModel _currentWarehouse;

    public WarehouseViewModel CurrentWarehouse
    {
        get => _currentWarehouse;
        set
        {
            _currentWarehouse = value;
            BindingContext = CurrentWarehouse;
        }
    }

    public WarehouseDetailsPage()
    {
        InitializeComponent();
    }
}