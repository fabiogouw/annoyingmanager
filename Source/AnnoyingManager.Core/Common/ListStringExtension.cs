using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Common
{
    public static class ListStringExtension
    {
        public static void AddIfNotExists(this List<string> list, string value)
        {
            if (!string.IsNullOrEmpty(value) && !list.Contains(value))
            {
                list.Add(value);
            }
        }

        public static void AddIfNotExists(this List<string> list, IEnumerable<string> values)
        {
            foreach(string value in values)
            {
                AddIfNotExists(list, value);
            }
        }
    }
}
