using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab.Helpers
{
    public static class EnumHelper
    {
        public static string[] EnumToStringArray<T>() where T : Enum
        {
            return Enum.GetNames(typeof(T));
        }
    }
}
