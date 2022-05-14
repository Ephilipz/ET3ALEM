using System;

namespace Helpers.Extensions
{
    public static class ObjectExtensions
    {
        public static T GetValue<T>(this object obj, string propertyName)
        {
            var prop = obj?.GetType().GetProperty(propertyName);
            if (prop is null)
            {
                return default;
            }
            var val = prop?.GetValue(obj, null);
            return (T)Convert.ChangeType(val, prop.PropertyType);
        }
    }
}