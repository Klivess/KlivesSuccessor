using KlivesSuccessor.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlivesSuccessor.Interaction
{
    class BeginApp
    {
        public bool isWhite = false;
        public void StartApp()
        {
            while (true)
            {
                Console.WriteLine("KlivesSuccessor to play Black (b) or White (w)?");
                string option = Console.ReadLine();
                if (option != null && (option.ToLower() == "w" || option.ToLower() == "b"))
                {
                    Console.WriteLine("Setting up game from starting position.");
                    if (option == "w")
                    {
                        isWhite = true;
                    }
                    break;
                }
            }
            BeginGame();
        }
        public void BeginGame()
        {

        }
    }
}
