using ProductManager.ViewModels;

namespace ProductManager.UI.Pages;

// Приймаємо параметр "SelectedProduct", який ми передали з попередньої сторінки
[QueryProperty(nameof(CurrentProduct), "SelectedProduct")]
public partial class ProductDetailsPage : ContentPage
{
    private ProductViewModel _currentProduct;

    public ProductViewModel CurrentProduct
    {
        get => _currentProduct;
        set
        {
            _currentProduct = value;
            BindingContext = CurrentProduct;
        }
    }

    public ProductDetailsPage()
    {
        InitializeComponent();
    }
}