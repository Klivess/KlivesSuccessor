using KlivesSuccessor.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlivesSuccessor.Engine;

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
            Random rnd = new();
            Models.ChessGame game = new Models.ChessGame();
            game.ConstructChessGame();
            bool isWhite = true;
            for (int i = 0; i < 20; i++)
            {
                List<KeyValuePair<PieceIntelligence.Piece, Models.ChessCoordinates>> availableChessMoves = new();
                var pieces = isWhite ? game.WhitePieces : game.BlackPieces;
                foreach (var item in pieces)
                {
                    var moves = item.CalculateMoveSquares();
                    foreach (var item2 in moves)
                    {
                        availableChessMoves.Add(new KeyValuePair<PieceIntelligence.Piece, Models.ChessCoordinates>(item, item2));
                    }
                }
                var move = availableChessMoves.ElementAt(rnd.Next(0, availableChessMoves.Count));
                game.CreateMove(move.Key, move.Value);
            }
            Console.WriteLine(game.PGN);
        }
    }
}
