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

            public ChessGame ConstructChessGame()
            {
                //Create white pieces in starting position
                Piece whiteRook1 = new Piece().ConstructPiece(PieceType.Rook, this, new ChessCoordinates().ConstructCoordinates(1, 1), true);
                Piece whiteKnight1 = new Piece().ConstructPiece(PieceType.Knight, this, new ChessCoordinates().ConstructCoordinates(2, 1), true);
                Piece whiteBishop1 = new Piece().ConstructPiece(PieceType.Bishop, this, new ChessCoordinates().ConstructCoordinates(3, 1), true);
                Piece whiteQueen = new Piece().ConstructPiece(PieceType.Queen, this, new ChessCoordinates().ConstructCoordinates(4, 1), true);
                Piece whiteKing = new Piece().ConstructPiece(PieceType.King, this, new ChessCoordinates().ConstructCoordinates(5, 1), true);
                Piece whiteBishop2 = new Piece().ConstructPiece(PieceType.Bishop, this, new ChessCoordinates().ConstructCoordinates(6, 1), true);
                Piece whiteKnight2 = new Piece().ConstructPiece(PieceType.Knight, this, new ChessCoordinates().ConstructCoordinates(7, 1), true);
                Piece whiteRook2 = new Piece().ConstructPiece(PieceType.Rook, this, new ChessCoordinates().ConstructCoordinates(8, 1), true);
                List<Piece> whitePieces = new();
                whitePieces.Add(whiteRook1);
                whitePieces.Add(whiteKnight1);
                whitePieces.Add(whiteBishop1);
                whitePieces.Add(whiteQueen);
                whitePieces.Add(whiteKing);
                whitePieces.Add(whiteBishop2);
                whitePieces.Add(whiteKnight2);
                whitePieces.Add(whiteRook2);
                //Construct white pawns
                for (int i = 1; i <= 8; i++)
                {
                    Piece pawn = new Piece().ConstructPiece(PieceType.Pawn, this, new ChessCoordinates().ConstructCoordinates(i, 2), true);
                    whitePieces.Add(pawn);
                }
                WhitePieces = whitePieces.ToArray();
                //Create black pieces in starting position
                Piece blackRook1 = new Piece().ConstructPiece(PieceType.Rook, this, new ChessCoordinates().ConstructCoordinates(1, 8), true);
                Piece blackKnight1 = new Piece().ConstructPiece(PieceType.Knight, this, new ChessCoordinates().ConstructCoordinates(2, 8), true);
                Piece blackBishop1 = new Piece().ConstructPiece(PieceType.Bishop, this, new ChessCoordinates().ConstructCoordinates(3, 8), true);
                Piece blackQueen = new Piece().ConstructPiece(PieceType.Queen, this, new ChessCoordinates().ConstructCoordinates(4, 8), true);
                Piece blackKing = new Piece().ConstructPiece(PieceType.King, this, new ChessCoordinates().ConstructCoordinates(5, 8), true);
                Piece blackBishop2 = new Piece().ConstructPiece(PieceType.Bishop, this, new ChessCoordinates().ConstructCoordinates(6, 8), true);
                Piece blackKnight2 = new Piece().ConstructPiece(PieceType.Knight, this, new ChessCoordinates().ConstructCoordinates(7, 8), true);
                Piece blackRook2 = new Piece().ConstructPiece(PieceType.Rook, this, new ChessCoordinates().ConstructCoordinates(8, 8), true);
                List<Piece> blackPieces = new();
                blackPieces.Add(blackRook1);
                blackPieces.Add(blackKnight1);
                blackPieces.Add(blackBishop1);
                blackPieces.Add(blackQueen);
                blackPieces.Add(blackKing);
                blackPieces.Add(blackBishop2);
                blackPieces.Add(blackKnight2);
                blackPieces.Add(blackRook2);
                //Construct black pawns
                for (int i = 1; i <= 8; i++)
                {
                    Piece pawn = new Piece().ConstructPiece(PieceType.Pawn, this, new ChessCoordinates().ConstructCoordinates(i, 7), true);
                    blackPieces.Add(pawn);
                }
                BlackPieces = blackPieces.ToArray();
                return this;
            }

            public string CalculateFEN()
            {
                //uncompleted
                return "";
            }

            public void CreateMove(Piece piece, ChessCoordinates newCoord)
            {
                if (newCoord.X > 8 || newCoord.Y > 8)
                {
                    throw new Exception("New coordinates is out of bounds of boardspace.");
                }
                else
                {
                    Piece? attackedpiece = GetPieceAtPosition(newCoord);
                    if (attackedpiece != null)
                    {
                        RemovePieceOfID((Piece)attackedpiece);
                    }
                    piece.Position = newCoord;
                }
            }

            public Piece? GetPieceAtPosition(ChessCoordinates coordinate)
            {
                var pieces = BlackPieces.Concat(WhitePieces);
                foreach(var item in pieces)
                {
                    if (PieceIntelligence.ArePiecePositionsEqual(item.Position, coordinate))
                    {
                        return item;
                    }
                }
                return null;
            }
            private void RemovePieceOfID(Piece piece)
            {
                if (piece.pieceIsWhite)
                {
                    int indexOfPiece = WhitePieces.Select(k => k.ID).ToList().IndexOf(piece.ID);
                    var newList = WhitePieces.ToList();
                    newList.RemoveAt(indexOfPiece);
                    WhitePieces = newList.ToArray();
                }
                else
                {
                    int indexOfPiece = BlackPieces.Select(k => k.ID).ToList().IndexOf(piece.ID);
                    var newList = BlackPieces.ToList();
                    newList.RemoveAt(indexOfPiece);
                    BlackPieces = newList.ToArray();
                }
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
