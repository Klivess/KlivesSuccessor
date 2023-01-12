using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlivesSuccessor.Engine
{
    class Utilities
    {
        public static string GenerateRandomString(int length = 5)
        {
            Random rnd = new();
            string str = "";
            for (int i = 0; i <= 5; i++)
            {
                str += rnd.Next(0, 9).ToString();
            }
            return str;
        }
    }
}
