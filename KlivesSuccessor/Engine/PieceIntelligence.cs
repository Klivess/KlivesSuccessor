using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KlivesSuccessor.Engine.Models;

namespace KlivesSuccessor.Engine
{
    class PieceIntelligence
    {
        public enum PieceType
        {
            Pawn,
            Bishop,
            Knight,
            Rook, 
            Queen,
        }
        private static int[] PieceValues =
        {
            1,
            3,
            3,
            5,
            9
        };
        public static bool ArePiecePositionsEqual(ChessCoordinates pos1, ChessCoordinates pos2)
        {
            return pos1.TranslatePositionToText() == pos2.TranslatePositionToText();
        }
        public struct Piece
        {
            public ChessGame OwningChessGame;
            public PieceType Type;
            public ChessCoordinates Position;
            public ChessCoordinates[] AvailableAttackingSquares;
            public bool pieceIsWhite;

            //Piece constructor
            public Piece ConstructPiece(PieceType type, ChessGame chessGame, ChessCoordinates position, bool isWhite)
            {
                Type = type;
                OwningChessGame = chessGame;
                Position = position;
                pieceIsWhite = isWhite;
                AvailableAttackingSquares = CalculateMoveSquares();
                return this;
            }
            public int AcquirePieceValue()
            {
                return PieceValues[(int)Type];
            }
            public ChessCoordinates[] CalculateMoveSquares()
            {
                var allOccupiedSquares = OwningChessGame.CalculateOccupiedSquares();
                List<ChessCoordinates> coords = new();
                switch (Type)
                {
                    case PieceType.Pawn:
                        bool canMoveTwo = false;
                        if (pieceIsWhite)
                        {
                            canMoveTwo = Position.Y == 2;
                        }
                        else
                        {
                            canMoveTwo = Position.Y == 7;
                        }
                        List<ChessCoordinates> IgnorantPieceMoves = new();
                        var firstMove = new ChessCoordinates().ConstructCoordinates(Position.X, Position.Y + 1);
                        var secondMove = new ChessCoordinates().ConstructCoordinates(Position.X, Position.Y + 2);
                        IgnorantPieceMoves.Add(firstMove);
                        if (canMoveTwo && allOccupiedSquares.Contains(secondMove) == false)
                        {
                            IgnorantPieceMoves.Add(secondMove);
                        }
                        if (allOccupiedSquares.Contains(firstMove))
                        {
                            IgnorantPieceMoves.Remove(firstMove);
                        }
                        coords = IgnorantPieceMoves;
                        break;
                    case PieceType.Bishop:
                        //North East Diagonal
                        for (int i = 0; i <= 8; i++)
                        {

                        }
                        //North West Diagonal
                        for (int i = 0; i <= 8; i++)
                        {

                        }
                        //South East Diagonal
                        for (int i = 0; i <= 8; i++)
                        {

                        }
                        //South West Diagonal
                        for (int i = 0; i <= 8; i++)
                        {

                        }
                        break;
                    case PieceType.Knight:
                        break;
                }
                return coords.ToArray();
            }
        }
    }
}
