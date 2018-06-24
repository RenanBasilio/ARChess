using UnityEngine;
using UnityEngine.Rendering;

namespace Chess
{
    public class Tile : MonoBehaviour {
		public Piece piece;
		private GameObject displayCube;

		public Pair<int, int> position;

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
		}

		public void setPosition(int row, int column) {
			position = new Pair<int, int>(row, column);
		}

		public Piece getPiece() {
			return piece;
		}

		public void setPiece(Piece piece) {
			this.piece = piece;
			this.piece.transform.SetParent(transform, false);
		}

		public bool hasPiece() {
			return (piece == null)? true : false;
		}

		public void enableDisplay(Player player) {
            if (piece != null) {
                if (piece.owner != player) {
                    displayCube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    			    displayCube.SetActive(true);
                }
                else return;
            }
            else {
                displayCube.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                displayCube.SetActive(true);
            }

		}

        public void disableDisplay() {
            this.displayCube.SetActive(false);
        }

		public bool isInCheck() {
			return false;
		}
	}
}