using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KlivesSuccessor.Engine.PieceIntelligence;

namespace KlivesSuccessor.Engine
{
    class Models
    {
        public struct ChessGame
        {
            public string FEN;
            public string PGN;
            public Piece[] WhitePieces;
            public Piece[] BlackPieces;

            public ChessCoordinates[] CalculateOccupiedSquares()
            {
                List<ChessCoordinates> coords = new();
                WhitePieces.ToList().ForEach(x => coords.Add(x.Position));
                BlackPieces.ToList().ForEach(x => coords.Add(x.Position));
                return coords.ToArray();
            }
        }
        public struct ChessCoordinates
        {
            public int X { get; set; }
            public int Y { get; set; }
            public ChessCoordinates ConstructCoordinates(int x, int y)
            {
                X = Math.Clamp(Convert.ToInt32(x), 1, 8);
                Y = Math.Clamp(Convert.ToInt32(x), 1, 8);
                return this;
            }
            public string TranslatePositionToText()
            {
                string[] alphabet = { "a", "b", "c", "d", "e", "g", "h" };
                string position = alphabet[(Convert.ToInt32(Y) - 1)] + X;
                return position;
            }
        }
    }
}
