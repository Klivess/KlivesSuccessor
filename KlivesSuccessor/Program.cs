using KlivesSuccessor.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KlivesSuccessor
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            BeginApp beginApp = new BeginApp();
            beginApp.StartApp();
        }
    }
}
