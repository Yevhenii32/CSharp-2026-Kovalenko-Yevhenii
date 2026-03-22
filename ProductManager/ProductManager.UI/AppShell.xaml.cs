using ProductManager.UI.Pages;

namespace ProductManager.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseDetailsPage)}", typeof(WarehouseDetailsPage));
            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseDetailsPage)}/{nameof(ProductDetailsPage)}", typeof(ProductDetailsPage));
        }
    }
}