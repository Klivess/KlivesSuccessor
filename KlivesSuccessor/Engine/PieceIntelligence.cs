using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
            King,
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
            public string ID;
            public ChessGame OwningChessGame;
            public PieceType Type;
            public ChessCoordinates Position;
            public bool pieceIsWhite;

            //Piece constructor
            public Piece ConstructPiece(PieceType type, ChessGame chessGame, ChessCoordinates position, bool isWhite)
            {
                Type = type;
                OwningChessGame = chessGame;
                Position = position;
                pieceIsWhite = isWhite;
                ID = Utilities.GenerateRandomString();
                if (isWhite)
                {
                    chessGame.WhitePieces.Add(this);
                }
                else
                {
                    chessGame.BlackPieces.Add(this);
                }
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
                if (Type == PieceType.Pawn)
                {
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
                    ChessCoordinates firstMove;
                    ChessCoordinates secondMove;
                    if (pieceIsWhite)
                    {
                        firstMove = new ChessCoordinates().ConstructCoordinates(Position.X, Position.Y + 1);
                        secondMove = new ChessCoordinates().ConstructCoordinates(Position.X, Position.Y + 2);
                    }
                    else
                    {
                        firstMove = new ChessCoordinates().ConstructCoordinates(Position.X, Position.Y - 1);
                        secondMove = new ChessCoordinates().ConstructCoordinates(Position.X, Position.Y - 2);
                    }
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
                    coords.Add(firstMove);
                }
                else if (Type==PieceType.Bishop)
                {
                    //North East Diagonal
                    for (int i = 1; i <= 8 - Position.Y; i++)
                    {
                        ChessCoordinates pos = new ChessCoordinates().ConstructCoordinates(Position.X + i, Position.Y + i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        else
                        {
                            coords.Add(pos);
                        }
                    }
                    //North West Diagonal
                    for (int i = 1; i <= 8 - Position.Y; i++)
                    {
                        ChessCoordinates pos = new ChessCoordinates().ConstructCoordinates(Position.X - i, Position.Y + i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        else
                        {
                            coords.Add(pos);
                        }
                    }
                    //South East Diagonal
                    for (int i = 1; i <= 8 - Position.Y; i++)
                    {
                        ChessCoordinates pos = new ChessCoordinates().ConstructCoordinates(Position.X + i, Position.Y - i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        else
                        {
                            coords.Add(pos);
                        }
                    }
                    //South West Diagonal
                    for (int i = 1; i <= 8 - Position.Y; i++)
                    {
                        ChessCoordinates pos = new ChessCoordinates().ConstructCoordinates(Position.X - i, Position.Y + -i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        else
                        {
                            coords.Add(pos);
                        }
                    }
                }
                else if (Type == PieceType.Knight)
                {
                    ChessCoordinates pos1 = new ChessCoordinates().ConstructCoordinates(Position.Y + 2, Position.X - 1);
                    ChessCoordinates pos2 = new ChessCoordinates().ConstructCoordinates(Position.Y + 2, Position.X - 1);
                    ChessCoordinates pos3 = new ChessCoordinates().ConstructCoordinates(Position.Y - 2, Position.X - 1);
                    ChessCoordinates pos4 = new ChessCoordinates().ConstructCoordinates(Position.Y - 2, Position.X + 1);
                    ChessCoordinates pos5 = new ChessCoordinates().ConstructCoordinates(Position.Y + 1, Position.X - 2);
                    ChessCoordinates pos6 = new ChessCoordinates().ConstructCoordinates(Position.Y + 1, Position.X + 2);
                    ChessCoordinates pos7 = new ChessCoordinates().ConstructCoordinates(Position.Y - 1, Position.X - 2);
                    ChessCoordinates pos8 = new ChessCoordinates().ConstructCoordinates(Position.Y - 1, Position.X + 2);
                    coords.Add(pos1);
                    coords.Add(pos2);
                    coords.Add(pos3);
                    coords.Add(pos4);
                    coords.Add(pos5);
                    coords.Add(pos6);
                    coords.Add(pos7);
                    coords.Add(pos8);
                    ChessCoordinates[] coordCopy = new ChessCoordinates[coords.ToArray().Length];
                    coords.CopyTo(0, coordCopy, 0, coords.Count);
                    foreach (var item in coordCopy)
                    {
                        if (allOccupiedSquares.Contains(item))
                        {
                            coords.Remove(item);
                        }
                    }
                }
                else if (Type == PieceType.Rook)
                {
                    //Upwards
                    for (int i = 1; i <= 8; i++)
                    {
                        ChessCoordinates pos = new();
                        pos.ConstructCoordinates(Position.Y + i, Position.X);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        coords.Add(pos);
                    }
                    //Left
                    for (int i = 1; i <= 8; i++)
                    {
                        ChessCoordinates pos = new();
                        pos.ConstructCoordinates(Position.Y, Position.X - i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        coords.Add(pos);
                    }
                    //Right
                    for (int i = 1; i <= 8; i++)
                    {
                        ChessCoordinates pos = new();
                        pos.ConstructCoordinates(Position.Y, Position.X + i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        coords.Add(pos);
                    }
                    //Downwards
                    for (int i = 1; i <= 8; i++)
                    {
                        ChessCoordinates pos = new();
                        pos.ConstructCoordinates(Position.Y - i, Position.X);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        coords.Add(pos);
                    }
                }
                else if (Type == PieceType.Queen)
                {
                    //Upwards
                    for (int i = 1; i <= 8; i++)
                    {
                        ChessCoordinates pos = new();
                        pos.ConstructCoordinates(Position.Y + i, Position.X);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        coords.Add(pos);
                    }
                    //Left
                    for (int i = 1; i <= 8; i++)
                    {
                        ChessCoordinates pos = new();
                        pos.ConstructCoordinates(Position.Y, Position.X - i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        coords.Add(pos);
                    }
                    //Right
                    for (int i = 1; i <= 8; i++)
                    {
                        ChessCoordinates pos = new();
                        pos.ConstructCoordinates(Position.Y, Position.X + i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        coords.Add(pos);
                    }
                    //Downwards
                    for (int i = 1; i <= 8; i++)
                    {
                        ChessCoordinates pos = new();
                        pos.ConstructCoordinates(Position.Y - i, Position.X);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        coords.Add(pos);
                    }
                    //North East Diagonal
                    for (int i = 1; i <= 8 - Position.Y; i++)
                    {
                        ChessCoordinates pos = new ChessCoordinates().ConstructCoordinates(Position.X + i, Position.Y + i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        else
                        {
                            coords.Add(pos);
                        }
                    }
                    //North West Diagonal
                    for (int i = 1; i <= 8 - Position.Y; i++)
                    {
                        ChessCoordinates pos = new ChessCoordinates().ConstructCoordinates(Position.X - i, Position.Y + i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        else
                        {
                            coords.Add(pos);
                        }
                    }
                    //South East Diagonal
                    for (int i = 1; i <= 8 - Position.Y; i++)
                    {
                        ChessCoordinates pos = new ChessCoordinates().ConstructCoordinates(Position.X + i, Position.Y - i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        else
                        {
                            coords.Add(pos);
                        }
                    }
                    //South West Diagonal
                    for (int i = 1; i <= 8 - Position.Y; i++)
                    {
                        ChessCoordinates pos = new ChessCoordinates().ConstructCoordinates(Position.X - i, Position.Y + -i);
                        if (allOccupiedSquares.Contains(pos))
                        {
                            break;
                        }
                        else
                        {
                            coords.Add(pos);
                        }
                    }
                }
                var coordsCopy = coords;
                foreach(var item in coordsCopy)
                {
                    if (item.X > 8 || item.Y > 8)
                    {
                        coords.Remove(item);
                    }
                }
                return coords.ToArray();
            }
        }
    }
}
