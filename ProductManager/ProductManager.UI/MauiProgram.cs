using Microsoft.Extensions.Logging;
using ProductManager.UI.Pages;
using ProductManager.Storage;
using ProductManager.Repository;
using ProductManager.Services;
using ProductManager.UI.ViewModels;
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

            // Реєструємо шар даних (Storage) 
            builder.Services.AddSingleton<IStorageContext, StorageContext>();

            // Реєструємо шар репозиторіїв (Repository)
            builder.Services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();

            // Реєструємо шар бізнес-логіки (Services)
            builder.Services.AddTransient<IWarehouseService, WarehouseService>();
            builder.Services.AddTransient<IProductService, ProductService>();

            // Реєструємо ViewModels 
            builder.Services.AddTransient<WarehousesViewModel>();
            builder.Services.AddTransient<WarehouseDetailsViewModel>();
            builder.Services.AddTransient<ProductDetailsViewModel>();

            // Реєструємо сторінки (UI)
            builder.Services.AddSingleton<WarehousesPage>();
            builder.Services.AddTransient<WarehouseDetailsPage>();
            builder.Services.AddTransient<ProductDetailsPage>();

            return builder.Build();
        }
    }
}