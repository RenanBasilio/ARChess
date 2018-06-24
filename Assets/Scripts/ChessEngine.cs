using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess;

[System.Serializable]
public class ChessEngine : MonoBehaviour {

	public enum GamePhase { Init, WhiteMove, BlackMove };

	public GamePhase currentPhase = GamePhase.Init;

	public List<Transform> whitePiecePrefabs;
	public List<Transform> blackPiecePrefabs;

	private PieceType[] setup = new PieceType[] {PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen, PieceType.King, PieceType.Bishop, PieceType.Knight, PieceType.Rook};

	// Tile[Row, Column]
	public Tile[,] chessboard;
	public Dictionary<Player, List<Piece>> lostPieces;

	public Piece activePiece;
	public Tile activePieceTile;
	public List<Tile> activeTiles;

	public void Start(){
		activeTiles = new List<Tile>();

		Transform board = GameObject.Find("Board").transform;
		chessboard = new Tile[8,8];

		lostPieces = new Dictionary<Player, List<Piece>>();
		lostPieces.Add(Player.Black, new List<Piece>());
		lostPieces.Add(Player.White, new List<Piece>());

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
			OnRaycastHit(hit);
		}
		return -1;
	}

	public void OnRaycastHit(RaycastHit hit) {
		Tile tileHit = hit.transform.GetComponentInParent<Tile>();

		if (tileHit.isActive()) {
			Piece taken = activePiece.Move(activePieceTile, tileHit);
			if (taken != null) lostPieces[taken.owner].Add(taken);
			DisableTiles();
			ControlFlowNext();
		}
		else {
			if ((currentPhase == GamePhase.BlackMove && tileHit.getPiece().owner == Player.Black) || 
				(currentPhase == GamePhase.WhiteMove && tileHit.getPiece().owner == Player.White)) 
			{
				DisableTiles();
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
				activePiece = tileHit.getPiece();
				activePieceTile = tileHit;
			}
		}
	}

	public void SetupPieces() {

		lostPieces[Player.Black].Clear();
		lostPieces[Player.White].Clear();
		//chessboard[3,4].setPiece(Instantiate(blackPiecePrefabs[(int)PieceType.Pawn].GetComponent<Piece>()));
		
		for (int i = 0; i < 8; i++) {
			// White piece
			chessboard[0, i].setPiece(Instantiate(whitePiecePrefabs[(int)setup [i]].GetComponent<Piece>()));
			// White pawn
			chessboard[1, i].setPiece(Instantiate(whitePiecePrefabs[(int)PieceType.Pawn].GetComponent<Piece>()));
			// Black pawn
			chessboard[6, i].setPiece(Instantiate(blackPiecePrefabs[(int)PieceType.Pawn].GetComponent<Piece>()));
			// Black piece
			chessboard[7, i].setPiece(Instantiate(blackPiecePrefabs[(int)setup [i]].GetComponent<Piece>()));
		}
		
		ControlFlowNext();
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

	public void ControlFlowNext() {
		switch (currentPhase)
		{
			case GamePhase.WhiteMove:
				currentPhase = GamePhase.BlackMove;
				break;
			case GamePhase.BlackMove:
				currentPhase = GamePhase.WhiteMove;
				break;
			default:
				currentPhase = GamePhase.WhiteMove;
				break;
		}
	}

	private void DisableTiles() {
		foreach (Tile tile in activeTiles)
		{
			tile.disableDisplay();
		}
	}
}
