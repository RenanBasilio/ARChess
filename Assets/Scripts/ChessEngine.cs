using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

[System.Serializable]
public class ChessEngine : MonoBehaviour {

	public List<Transform> whitePiecePrefabs;
	public List<Transform> blackPiecePrefabs;

	private PieceType[] setup = new PieceType[] {PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen, PieceType.King, PieceType.Bishop, PieceType.Knight, PieceType.Rook};

	// Tile[Row, Column]
	public Tile[,] chessboard;
	public Dictionary<Player, List<Tile>> lostPieces;

	public List<Tile> activeTiles;

	public void Start(){
		activeTiles = new List<Tile>();

		Transform board = GameObject.Find("Board").transform;
		chessboard = new Tile[8,8];

		lostPieces = new Dictionary<Player, List<Tile>>();
		lostPieces.Add(Player.Black, new List<Tile>());
		lostPieces.Add(Player.White, new List<Tile>());

		for ( int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				GameObject tileObject = new GameObject();
				Tile tile = tileObject.AddComponent<Tile>();
				tile.setPosition(i, j);

				Vector3 worldPoint = ToWorldPoint(i, j);
				tileObject.transform.position = new Vector3(worldPoint.x, tile.transform.position.y, worldPoint.z);
				tileObject.transform.SetParent(board);

				chessboard[i, j] = tile;
			}
		}
		transform.SetParent(GameObject.Find("ChessTarget").transform, false);
	}

	public int RaycastCell(Ray ray) {
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			Tile tileHit = hit.transform.GetComponentInParent<Tile>();
			////////////////////////////////////////////////////////////////////////////
			
			foreach (Tile tile in activeTiles)
			{
				tile.disableDisplay();
			}
			activeTiles.Clear();
			activeTiles.AddRange(
				tileHit
					// Get piece hit
					.getPiece()
					// Get movement function from piece hit
					.getMoveMethods()
					// Call movement function
					.Invoke(chessboard, tileHit.position.First, tileHit.position.Second));
			foreach (Tile tile in activeTiles)
			{
				Debug.Log("Enabling tile [" + tile.position.First + "," + tile.position.Second + "]");
				tile.enableDisplay(tileHit.getPiece().owner);
			}
			/*
			foreach (Pair<int, int> item in possibleMoves)
			{
				if (tileHit.position.First + item.First <= 7 
					&& tileHit.position.Second + item.Second <=7
					&& tileHit.position.First + item.First >= 0
					&& tileHit.position.Second + item.Second >= 0)
					{
						chessboard[tileHit.position.First + item.First, tileHit.position.Second + item.Second].enableDisplay(tileHit.getPiece().owner);
						activeTiles.Add(chessboard[tileHit.position.First + item.First, tileHit.position.Second + item.Second]);
					}
			}*/
			////////////////////////////////////////////////////////////////////////////
			return 1;
		}
		return -1;
	}

	public void SetupPieces() {

		lostPieces[Player.Black].Clear();
		lostPieces[Player.White].Clear();
		chessboard[3,4].setPiece(new Piece(Player.Black, PieceType.Pawn, GameObject.Instantiate (blackPiecePrefabs [(int)PieceType.Pawn])));
		chessboard[4,5].setPiece(new Piece(Player.White, PieceType.Bishop, GameObject.Instantiate (whitePiecePrefabs [(int)PieceType.Pawn])));
		/*
		for (int i = 0; i < 8; i++) {
			// White piece
			chessboard[0, i].setPiece(new Piece(Player.White, setup[i], GameObject.Instantiate (whitePiecePrefabs [(int)setup [i]])));
			// White pawn
			//chessboard[1, i].setPiece(new Piece(Player.White, PieceType.Pawn, GameObject.Instantiate (whitePiecePrefabs[(int)PieceType.Pawn])));
			// Black pawn
			chessboard[6, i].setPiece(new Piece(Player.Black, PieceType.Pawn, GameObject.Instantiate (blackPiecePrefabs[(int)PieceType.Pawn])));
			// Black piece
			chessboard[7, i].setPiece(new Piece(Player.Black, setup[i], GameObject.Instantiate (blackPiecePrefabs [(int)setup [i]])));
		}*/
		

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
