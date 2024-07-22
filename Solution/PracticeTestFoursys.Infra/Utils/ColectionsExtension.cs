using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestFoursys.Infra.Utils
{
    public static class ColectionsExtension
    {
        public static bool Exist<T>(this IEnumerable<T> list)
            => list?.Any() ?? false;

        public static bool Exist<T>(this IEnumerable<T> list, Func<T, bool> predicate)
            => list?.Any(predicate) ?? false;

        public static bool NullorEmpty<T>(this IEnumerable<T> list)
            => list is null || !list.Exist();

        public static List<T> AddRange<T>(this List<T> listSource, params T[][] listParameters)
        {
            foreach (var list in listParameters)
            {
                listSource.AddRange(list);
            }

            return listSource.ToList();
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> listSource, IEnumerable<T> list)
        {
            if (listSource is null)
                return null;

            if (list is null)
                return listSource;

            return listSource?.Except(list);
        }
    }
}
