using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Chess
{
    public class Tile : MonoBehaviour {
		public Piece piece;
		private GameObject displayCube;

		private bool active = false;

		public Pair<int, int> position;

		private Dictionary<Player, List<Piece>> keepingInCheck;

		void Start() {

			this.displayCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			this.displayCube.transform.SetParent(transform, false);
			this.displayCube.transform.position = new Vector3(
				displayCube.transform.position.x,
				displayCube.transform.position.y + 0.1f,
				displayCube.transform.position.z);
			this.displayCube.transform.localScale = new Vector3(3, 1, 3);
			this.displayCube.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
			this.displayCube.SetActive(false);
			keepingInCheck = new Dictionary<Player, List<Piece>>();
			keepingInCheck.Add(Player.Black, new List<Piece>());
			keepingInCheck.Add(Player.White, new List<Piece>());
		}

		public void setPosition(int row, int column) {
			position = new Pair<int, int>(row, column);
		}

		public Piece getPiece() {
			return piece;
		}

		public Piece setPiece(Piece piece) {
			this.piece = piece;
			if(piece != null) this.piece.transform.SetParent(transform, false);
			return getPiece();
		}

		public bool hasPiece() {
			return (piece == null)? false : true;
		}

		public void enableDisplay(Player player) {
            if (piece != null) {
                if (piece.owner != player) {
                    displayCube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
					active = true;
    			    displayCube.SetActive(true);
                }
                else return;
            }
            else {
				active = true;
                displayCube.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                displayCube.SetActive(true);
            }

		}

        public void disableDisplay() {
			active = false;
            this.displayCube.SetActive(false);
        }

		public void toggleDisplayCheck() {
			if (!displayCube.activeSelf){
				displayCube.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
				displayCube.SetActive(true);
			}
			else {
				displayCube.SetActive(false);
			}
		}

		public bool isInCheck(Player owner) {
			switch (owner)
			{
				case Player.Black:
					if (keepingInCheck[Player.White].Count > 0) return true;
					else return false;
				case Player.White:
					if (keepingInCheck[Player.Black].Count > 0) return true;
					else return false;
				default:
					return false;
			}
		}

		public bool isActive() {
			return active;
		}

		public void Reset() {
			piece = null;
			foreach (List<Piece> pieces in keepingInCheck.Values)
			{
				pieces.Clear();
			}
		}

		public void addCheck(Piece piece) {
			keepingInCheck[piece.owner].Add(piece);
		}

		public void removeCheck(Piece piece) {
			keepingInCheck[piece.owner].Remove(piece);
		}
	}
}