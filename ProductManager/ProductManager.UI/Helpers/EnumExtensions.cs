using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProductManager.UI.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            // Отримуємо інформацію про конкретне вибране поле
            var memberInfo = enumType.GetMember(enumValue.ToString());

            if (memberInfo.Length > 0)
            {
                // Намагаємось знайти атрибут DisplayAttribute над цим полем
                var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
                // Якщо атрибут знайдено то повертаємо значення властивості Name (українською)
                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }
            return enumValue.ToString();
        }
    }
}