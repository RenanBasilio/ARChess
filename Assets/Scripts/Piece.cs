using System;
using System.Collections.Generic;
using Chess;
using UnityEngine;

namespace Chess
{
    public enum PieceType {King=0, Queen=1, Rook=2, Knight=3, Bishop=4, Pawn=5};
    public enum Player {White=-1, Black=1}

    [System.Serializable]
    public class Piece {

        public PieceType type;
        public Player owner;
        public Transform transform;

        private static Dictionary<PieceType, List<Pair<int, int>>> moves;
        private bool firstMove;

        public Piece(Player owner, PieceType type, Transform transform) {
            this.owner = owner;
            this.type = type;
            this.transform = transform;
            firstMove = true;
            if(moves == null) moves = new Dictionary<PieceType, List<Pair<int, int>>>(); 
        }

        public List<Pair<int, int>> getPossibleMoves () {
            switch (type)
            {
                case PieceType.Pawn:
                    // Pawns can only move forward, and can only take enemy 
                    // pieces diagonally.
                    List<Pair<int, int>> pawnMoves;
                    pawnMoves = new List<Pair<int, int>>();
                    pawnMoves.Add(new Pair<int, int>(-1 * (int)owner, 0));
                    pawnMoves.Add(new Pair<int, int>(-1 * (int)owner, 1));
                    pawnMoves.Add(new Pair<int, int>(-1 * (int)owner, -1));

                    if (firstMove) pawnMoves.Add(new Pair<int, int>(-2 * (int)owner, 0));
                    return pawnMoves;

                case PieceType.Rook:
                    // Rooks can move forward, backward or sideways up to 7 moves.
                    if (moves.ContainsKey(PieceType.Rook)) return moves[PieceType.Rook];
                    else {
                        List<Pair<int, int>> rookMoves = new List<Pair<int, int>>();
                        for (int i = -7; i < 8; i++) {
                            if (i != 0) {
                                rookMoves.Add(new Pair<int, int>(i, 0));
                                rookMoves.Add(new Pair<int, int>(0, i));
                            }
                        }
                        moves.Add(PieceType.Rook, rookMoves);
                        return rookMoves;
                    }

                case PieceType.Bishop:
                    if (moves.ContainsKey(PieceType.Bishop)) return moves[PieceType.Bishop];
                    else {
                        List<Pair<int, int>> bishopMoves = new List<Pair<int, int>>();
                        for (int i = -7; i < 8; i++) {
                            if (i != 0) {
                                bishopMoves.Add(new Pair<int, int>(i, i));
                                bishopMoves.Add(new Pair<int, int>(i, -i));
                            }
                        }
                        moves.Add(PieceType.Bishop, bishopMoves);
                        return bishopMoves;
                    }

                case PieceType.Knight:
                    if (moves.ContainsKey(PieceType.Knight)) return moves[PieceType.Knight];
                    else {
                        List<Pair<int, int>> knightMoves = new List<Pair<int, int>>();
                        
                        knightMoves.Add(new Pair<int, int>(2, 1));
                        knightMoves.Add(new Pair<int, int>(2, -1));
                        knightMoves.Add(new Pair<int, int>(-2, 1));
                        knightMoves.Add(new Pair<int, int>(-2, -1));
                        knightMoves.Add(new Pair<int, int>(1, 2));
                        knightMoves.Add(new Pair<int, int>(1, -2));
                        knightMoves.Add(new Pair<int, int>(-1, 2));
                        knightMoves.Add(new Pair<int, int>(-1, -2));

                        moves.Add(PieceType.Knight, knightMoves);
                        return knightMoves;
                    }

                case PieceType.Queen:
                    if (moves.ContainsKey(PieceType.Queen)) return moves[PieceType.Queen];
                    else {
                        List<Pair<int, int>> queenMoves = new List<Pair<int, int>>();
                        for (int i = -7; i < 8; i++) {
                            if (i != 0) {
                                queenMoves.Add(new Pair<int, int>(i, 0));
                                queenMoves.Add(new Pair<int, int>(0, i));
                                queenMoves.Add(new Pair<int, int>(i, i));
                                queenMoves.Add(new Pair<int, int>(i, -i));
                            }
                        }
                        moves.Add(PieceType.Queen, queenMoves);
                        return queenMoves;
                    }

                case PieceType.King:
                    // King can move to any adjacent tile.
                    if (moves.ContainsKey(PieceType.King)) return moves[PieceType.King];
                    else {
                        List<Pair<int, int>> kingMoves = new List<Pair<int, int>>();
                        for (int i = -1; i < 1; i++) {
                            for (int j = -1; j < 1; j++ ) {
                                if (i != 0 && j != 0) kingMoves.Add(new Pair<int, int>(i, j));
                            }
                        }
                        moves.Add(PieceType.King, kingMoves);
                        return kingMoves;
                    }

                default:
                    return new List<Pair<int, int>>();
            }
        }

        public Func<Tile[,], int, int, List<Tile>> getMoveMethods() {
            switch (type)
            {
                case PieceType.Rook:
                    return movesRook;
                default:
                    return null;
            }
        }

        public static List<Tile> movesRook(Tile[,] chessboard, int pieceRow, int pieceColumn) {
            bool stop = false;
            Piece piece = chessboard[pieceRow, pieceColumn].getPiece();
            List<Tile> enabledTiles = new List<Tile>();

            int i = 1;
            while (!stop)
            {
                if (pieceColumn + i > 7 ) break;
                if (chessboard[pieceRow, pieceColumn+i].getPiece() != null) stop = true;
                enabledTiles.Add(chessboard[pieceRow, pieceColumn+i]);
                i++;
            }
            i = 1;
            stop = false;
            while (!stop)
            {
                if (pieceColumn - i < 0) break;
                if (chessboard[pieceRow, pieceColumn-i].getPiece() != null) stop = true;
                enabledTiles.Add(chessboard[pieceRow, pieceColumn-i]);
                i++;
            }
            i = 1;
            stop = false;
            while (!stop)
            {
                if (pieceRow + i > 7) break;
                if (chessboard[pieceRow+i, pieceColumn].getPiece() != null) stop = true;
                enabledTiles.Add(chessboard[pieceRow+i, pieceColumn]);
                i++;
            }
            i = 1;
            stop = false;
            while (!stop)
            {
                if (pieceRow - i < 0) break;
                if (chessboard[pieceRow-i, pieceColumn].getPiece() != null) stop = true;
                enabledTiles.Add(chessboard[pieceRow-i, pieceColumn]);
                i++;
            }
            return enabledTiles;
        }
    }
}