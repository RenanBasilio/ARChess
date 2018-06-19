using UnityEngine;

namespace Chess
{
    public enum PieceType {King=0, Queen=1, Rook=2, Knight=3, Bishop=4, Pawn=5};
    public enum Player {White=0, Black=1}

    public struct Piece {

        public PieceType type;
        public Player owner;
        public Transform transform;

        public Piece(Player owner, PieceType type, Transform transform) {
            this.owner = owner;
            this.type = type;
            this.transform = transform;
        }
    }
}