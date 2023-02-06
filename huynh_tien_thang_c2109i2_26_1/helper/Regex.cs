using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace huynh_tien_thang_c2109i2_26_1.helper
{
    internal class Regex
    {
        public static string hay = "/([a-zA-Z0-9]+)([\\.{1}])?([a-zA-Z0-9]+)\\@gmail([\\.])com/g";
        public static string phone  = @"(84|0[3|5|7|8|9])+([0-9]{8})\b";
        public static string gmail = @"([a-zA-Z0-9]+)([\.{1}])?([a-zA-Z0-9]+)\@gmail([\.])com";
    }
}
