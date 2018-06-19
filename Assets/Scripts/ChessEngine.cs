using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

public class ChessEngine : MonoBehaviour {

	public List<Transform> whitePiecePrefabs;
	public List<Transform> blackPiecePrefabs;

	private PieceType[] setup = new PieceType[] {PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen, PieceType.King, PieceType.Bishop, PieceType.Knight, PieceType.Rook};

	// Tile[Column, Row]
	public Tile[,] chessboard;
	public Dictionary<Player, List<Tile>> lostPieces;

	public void Start(){
		Transform board = GameObject.Find("Board").transform;
		chessboard = new Tile[8,8];

		lostPieces = new Dictionary<Player, List<Tile>>();
		lostPieces.Add(Player.Black, new List<Tile>());
		lostPieces.Add(Player.White, new List<Tile>());

		for ( int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				GameObject piece = new GameObject();

				Vector3 worldPoint = ToWorldPoint(i, j);
				piece.transform.position = new Vector3(worldPoint.x, piece.transform.position.y, worldPoint.z);
				piece.transform.SetParent(board);

				chessboard[i, j] = new Tile(piece, null);
			}
		}
		transform.SetParent(GameObject.Find("ChessTarget").transform, false);
	}

	public int RaycastCell(Ray ray) {
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			Vector3 point = hit.point + new Vector3 (-16, 0, 16);
			int i = (int)-point.x / 4;
			int j = (int)point.z / 4;
			return i * 8 + j;
		}
		return -1;
	}

	public void SetupPieces() {

		lostPieces[Player.Black].Clear();
		lostPieces[Player.White].Clear();

		for (int i = 0; i < 8; i++) {
			// White piece
			chessboard[0, i].setPiece(new Piece(Player.White, setup[i], GameObject.Instantiate (whitePiecePrefabs [(int)setup [i]])));
			// White pawn
			chessboard[1, i].setPiece(new Piece(Player.White, PieceType.Pawn, GameObject.Instantiate (whitePiecePrefabs[(int)PieceType.Pawn])));
			// Black pawn
			chessboard[6, i].setPiece(new Piece(Player.Black, PieceType.Pawn, GameObject.Instantiate (blackPiecePrefabs[(int)PieceType.Pawn])));
			// Black piece
			chessboard[7, i].setPiece(new Piece(Player.Black, setup[i], GameObject.Instantiate (blackPiecePrefabs [(int)setup [i]])));
		}

	}

	public static string GetCellString (int cellNumber)
	{
		int j = cellNumber % 8;
		int i = cellNumber / 8;
		return char.ConvertFromUtf32 (j + 65) + "" + (i + 1);
	}

	public static Vector3 ToWorldPoint(int row, int column) {

		return new Vector3 (row*-4+14, 1, column*4-14);
	}
}
