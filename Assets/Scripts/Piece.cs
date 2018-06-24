using System;
using System.Collections.Generic;
using Chess;
using UnityEngine;

namespace Chess
{
    public enum PieceType {King=0, Queen=1, Rook=2, Knight=3, Bishop=4, Pawn=5};
    public enum Player {White=-1, Black=1}

    [System.Serializable]
    public class Piece : MonoBehaviour {

        public PieceType type;
        public Player owner;

        private bool firstMove;

        public void Start() {
            firstMove = true;
        }

        public void Initialize(Player owner, PieceType type) {
            this.owner = owner;
            this.type = type;
            firstMove = true;
        }

        public void Update() {

        }

        public Func<Tile[,], int, int, List<Tile>> getMoveMethods() {
            switch (type)
            {
                case PieceType.Pawn:
                    return this.getPawnMoves;
                case PieceType.Knight:
                    return getKnightMoves;
                case PieceType.Rook:
                    return getRookMoves;
                case PieceType.Bishop:
                    return getBishopMoves;
                case PieceType.Queen:
                    return getQueenMoves;
                case PieceType.King:
                    return getKingMoves;
                default:
                    return null;
            }
        }

        public List<Tile> getPawnMoves(Tile[,] chessboard, int pieceRow, int pieceColumn) {
            List<Tile> moves = new List<Tile>();

            // Pawns can always move forward one space, unless there's an enemy piece on that spot;
            if (chessboard[pieceRow-(int)owner, pieceColumn].getPiece() == null)
                moves.Add(chessboard[pieceRow-((int)owner), pieceColumn]);

            // If it's the first move, pawns can move forward two spaces;
            if (firstMove && chessboard[pieceRow-(int)owner*2, pieceColumn].getPiece() == null) 
                moves.Add(chessboard[pieceRow-((int)owner)*2, pieceColumn]);

            // If an enemy piece is in a forward diagonal direction, pawn can take it;
            if ((pieceColumn+(int)owner < 7 && pieceColumn+(int)owner > 0) && chessboard[pieceRow-(int)owner, pieceColumn+(int)owner].getPiece() != null)
                moves.Add(chessboard[pieceRow-((int)owner), pieceColumn+((int)owner)]);

            if ((pieceColumn-(int)owner < 7 && pieceColumn-(int)owner > 0) && chessboard[pieceRow-(int)owner, pieceColumn-(int)owner].getPiece() != null)
                moves.Add(chessboard[pieceRow-((int)owner), pieceColumn-((int)owner)]);
            return moves;
        }

        public static List<Tile> getKnightMoves(Tile[,] chessboard, int pieceRow, int pieceColumn) {
            List<Tile> moves = new List<Tile>();

            if (pieceRow + 2 <= 7 && pieceColumn + 1 <= 7) moves.Add(chessboard[pieceRow+2, pieceColumn+1]);
            if (pieceRow + 2 <= 7 && pieceColumn - 1 >= 0) moves.Add(chessboard[pieceRow+2, pieceColumn-1]);
            if (pieceRow + 1 <= 7 && pieceColumn + 2 <= 7) moves.Add(chessboard[pieceRow+1, pieceColumn+2]);
            if (pieceRow + 1 <= 7 && pieceColumn - 2 >= 0) moves.Add(chessboard[pieceRow+1, pieceColumn-2]);
            if (pieceRow - 2 >= 0 && pieceColumn + 1 <= 7) moves.Add(chessboard[pieceRow-2, pieceColumn+1]);
            if (pieceRow - 2 >= 0 && pieceColumn - 1 >= 0) moves.Add(chessboard[pieceRow-2, pieceColumn-1]);
            if (pieceRow - 1 >= 0 && pieceColumn + 2 <= 7) moves.Add(chessboard[pieceRow-1, pieceColumn+2]);
            if (pieceRow - 1 >= 0 && pieceColumn - 2 >= 0) moves.Add(chessboard[pieceRow-1, pieceColumn-2]);

            return moves;
        }

        public static List<Tile> getRookMoves(Tile[,] chessboard, int pieceRow, int pieceColumn) {
            bool stop = false;
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

        public static List<Tile> getBishopMoves(Tile[,] chessboard, int pieceRow, int pieceColumn) {
            bool stop = false;
            Piece piece = chessboard[pieceRow, pieceColumn].getPiece();
            List<Tile> enabledTiles = new List<Tile>();

            int i = 1;
            while (!stop)
            {
                if (pieceRow + i > 7 || pieceColumn + i > 7 ) break;
                if (chessboard[pieceRow+i, pieceColumn+i].getPiece() != null) stop = true;
                enabledTiles.Add(chessboard[pieceRow+i, pieceColumn+i]);
                i++;
            }
            i = 1;
            stop = false;
            while (!stop)
            {
                if (pieceRow - i < 0 || pieceColumn - i < 0) break;
                if (chessboard[pieceRow-i, pieceColumn-i].getPiece() != null) stop = true;
                enabledTiles.Add(chessboard[pieceRow-i, pieceColumn-i]);
                i++;
            }
            i = 1;
            stop = false;
            while (!stop)
            {
                if (pieceRow + i > 7 ||  pieceColumn - i < 0) break;
                if (chessboard[pieceRow+i, pieceColumn-i].getPiece() != null) stop = true;
                enabledTiles.Add(chessboard[pieceRow+i, pieceColumn-i]);
                i++;
            }
            i = 1;
            stop = false;
            while (!stop)
            {
                if (pieceRow - i < 0 || pieceColumn + i > 7 ) break;
                if (chessboard[pieceRow-i, pieceColumn+i].getPiece() != null) stop = true;
                enabledTiles.Add(chessboard[pieceRow-i, pieceColumn+i]);
                i++;
            }
            return enabledTiles;
        }

        public static List<Tile> getQueenMoves(Tile[,] chessboard, int pieceRow, int pieceColumn) {
            List<Tile> moves = new List<Tile>();
            moves.AddRange(getRookMoves(chessboard, pieceRow, pieceColumn));
            moves.AddRange(getBishopMoves(chessboard, pieceRow, pieceColumn));
            return moves;
        }

        public static List<Tile> getKingMoves(Tile[,] chessboard, int pieceRow, int pieceColumn) {
            List<Tile> moves = new List<Tile>();
            if (pieceRow + 1 <= 7 && !chessboard[pieceRow+1, pieceColumn].isInCheck()) 
                moves.Add(chessboard[pieceRow+1, pieceColumn]);
            if (pieceRow - 1 >= 0 && !chessboard[pieceRow-1, pieceColumn].isInCheck()) 
                moves.Add(chessboard[pieceRow-1, pieceColumn]);
            if (pieceColumn + 1 <= 7 && !chessboard[pieceRow, pieceColumn+1].isInCheck()) 
                moves.Add(chessboard[pieceRow, pieceColumn+1]);
            if (pieceColumn - 1 >= 0 && !chessboard[pieceRow, pieceColumn-1].isInCheck()) 
                moves.Add(chessboard[pieceRow, pieceColumn-1]);

            if ((pieceRow + 1 <= 7 && pieceColumn + 1 <= 7) && !chessboard[pieceRow+1, pieceColumn+1].isInCheck()) 
                moves.Add(chessboard[pieceRow+1, pieceColumn+1]);
            if ((pieceRow - 1 >= 0 && pieceColumn - 1 >= 0) && !chessboard[pieceRow-1, pieceColumn-1].isInCheck()) 
                moves.Add(chessboard[pieceRow-1, pieceColumn-1]);
            if ((pieceRow + 1 <= 7 && pieceColumn - 1 >= 0) && !chessboard[pieceRow+1, pieceColumn-1].isInCheck()) 
                moves.Add(chessboard[pieceRow+1, pieceColumn-1]);
            if ((pieceRow - 1 >= 0 && pieceColumn + 1 <= 7) && !chessboard[pieceRow-1, pieceColumn+1].isInCheck()) 
                moves.Add(chessboard[pieceRow-1, pieceColumn+1]);

            return moves;
        }

        public Piece Move(Tile origin, Tile destination) {
            firstMove = false;
            Piece taken = null;
            if (destination.hasPiece())
            {
                taken = destination.getPiece();
                taken.gameObject.SetActive(false);
            }
            destination.setPiece(this);
            origin.setPiece(null);
            return taken;
        }
    }
}