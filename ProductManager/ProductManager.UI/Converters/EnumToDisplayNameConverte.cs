using Microsoft.Maui.Controls;
using ProductManager.UI.Helpers; 
using System;
using System.Globalization;

namespace ProductManager.UI.Converters
{
    public class EnumToDisplayNameConverter : IValueConverter
    {
        // Метод для перетворення даних з коду на екран
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Якщо значення порожнє то повертаємо пустий рядок
            if (value is null)
                return string.Empty;
            // Якщо це не Enum то повертаємо його текстовий вигляд
            if (value is not Enum castedEnum)
                return value.ToString() ?? string.Empty;

            return castedEnum.GetDisplayName();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}