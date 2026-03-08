using ProductManager.UI.Pages;

namespace ProductManager.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            
            Routing.RegisterRoute(nameof(WarehouseDetailsPage), typeof(WarehouseDetailsPage));
        }
    }
}