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
    private async void ProductsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ProductViewModel selectedProduct)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "SelectedProduct", selectedProduct }
            };

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", navigationParameter);

            // Очищаємо виділення
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}