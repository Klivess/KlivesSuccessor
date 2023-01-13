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

        public struct ChessMove
        {
            public Piece pieceMoved;
            public Piece? pieceTaken;
            public ChessCoordinates originalCoordinate;
            public ChessCoordinates newCoordinate;
            public bool isWhite;
            public string translatedCoordinate;
            public ChessMove(Piece movedPiece, ChessCoordinates originalCoord, ChessCoordinates newCoord, bool whiteMove, Piece? takenPiece = null)
            {
                pieceMoved = movedPiece;
                pieceTaken = takenPiece;
                originalCoordinate = originalCoord;
                newCoordinate = newCoord;
                pieceTaken = takenPiece;
                isWhite= whiteMove;
                translatedCoordinate = newCoord.TranslatePositionToText();
            }
        }
        public struct ChessGame
        {
            public string FEN;
            public string PGN;
            public List<Piece> WhitePieces;
            public List<Piece> BlackPieces;
            public List<ChessMove> GameMoves;
            public ChessCoordinates[] CalculateOccupiedSquares()
            {
                List<ChessCoordinates> coords = new();
                foreach(var item in WhitePieces.Concat(BlackPieces))
                {
                    coords.Add(item.Position);
                }
                return coords.ToArray();
            }

            public ChessGame ConstructChessGame()
            {
                GameMoves = new();
                WhitePieces = new List<Piece>();
                BlackPieces = new List<Piece>();
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
                    var oldPos = piece.Position;
                    piece.Position = newCoord;
                    ChessMove move = new(piece, oldPos, newCoord, piece.pieceIsWhite, attackedpiece);
                    GameMoves.Add(move);
                    RefreshPGN();
                }
            }

            public string RefreshPGN()
            {
                bool onOff = false;
                int count = 1;
                foreach(var move in GameMoves)
                {
                    if (onOff)
                    {
                        PGN += $" {move.newCoordinate.TranslatePositionToText(move.pieceMoved.Type)}\n";
                    }
                    else
                    {
                        PGN += $" {count}. " + move.newCoordinate.TranslatePositionToText(move.pieceMoved.Type);
                        count++;
                    }
                    onOff = !onOff;
                }
                return PGN;
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
                    WhitePieces.RemoveAt(indexOfPiece);
                }
                else
                {
                    int indexOfPiece = BlackPieces.Select(k => k.ID).ToList().IndexOf(piece.ID);
                    BlackPieces.RemoveAt(indexOfPiece);
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
                Y = Math.Clamp(Convert.ToInt32(y), 1, 8);
                return this;
            }
            public string TranslatePositionToText(PieceType? piece = null, bool hasTaken = false)
            {
                string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h" };
                string[] pieceNames = { "", "B", "N", "R", "Q", "K", "h" };
                int alphabetCharIndex = (Convert.ToInt32(X) - 1);
                string position = (piece == null ? "" : pieceNames[(int)piece]) + (hasTaken ? "x" : "") + alphabet[alphabetCharIndex] + Y;
                return position;
            }
        }
    }
}
