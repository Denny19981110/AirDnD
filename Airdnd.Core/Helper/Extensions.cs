using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airdnd.Core.Helper
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            var fi =value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0? attributes[0].Description : value.ToString();
        }
    }
}
