using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTA.Converters
{
    public static class ToShortConverter
    {
        public static bool Convert(uint value, out short convertedValue)
        {
            convertedValue = 0;
            if (value > Int16.MaxValue)
                return false;

            convertedValue=(short)value;
            return true;

        }
    }
}
