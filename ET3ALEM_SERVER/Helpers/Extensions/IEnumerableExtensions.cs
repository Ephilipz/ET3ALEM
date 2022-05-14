using System.Collections.Generic;
using System.Linq;

namespace Helpers.Extensions
{
    public static partial class IEnumerableExtensions
    {
        public static IEnumerable<T> GetDeletedElements<T>(this IEnumerable<T> list)
        {
            return list.Where(element => element.GetValue<int>("Id") < 0);
        }
        
        public static IEnumerable<T> GetAddedElements<T>(this IEnumerable<T> list)
        {
            return list.Where(element => element.GetValue<int>("Id") == 0);
        }
        
        public static IEnumerable<T> GetUpdatedElements<T>(this IEnumerable<T> list)
        {
            return list.Where(element => element.GetValue<int>("Id") > 0);
        }
    }
}