using Microsoft.Extensions.Logging;
using ProductManager.Services;
using ProductManager.UI.Pages;
namespace ProductManager.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Реєструємо наш сервіс сховища 
            builder.Services.AddSingleton<IStorageService, StorageService>();
          
            // Головна сторінка зі складами
            builder.Services.AddSingleton<WarehousesPage>();

            // Сторінки деталей
            builder.Services.AddTransient<WarehouseDetailsPage>();
            builder.Services.AddTransient<ProductDetailsPage>();

            return builder.Build();
        }
    }
}