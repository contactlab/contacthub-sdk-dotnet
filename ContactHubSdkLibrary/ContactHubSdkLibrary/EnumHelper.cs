using System;
using System.ComponentModel.DataAnnotations;

namespace ContactHubSdkLibrary
{
    public static class EnumHelper<T>
    {

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Get display value from enum item
        /// </summary>
        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        /// <summary>
        /// Get enum item from display name
        /// </summary>
        public static T GetValueFromDisplayName(string x)
        {
            try
            {
                T enumValue = ContactHubSdkLibrary.EnumHelper<T>.Parse(Common.MakeValidFileName(x));
                return enumValue;
            }
            catch
            {
                T enumValue = ContactHubSdkLibrary.EnumHelper<T>.Parse(Common.MakeValidFileName("NoValue"));
                return enumValue;
            }
        }
    }
}
