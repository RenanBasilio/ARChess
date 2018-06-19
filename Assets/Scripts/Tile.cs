using UnityEngine;

namespace Chess
{
    public class Tile {
		public GameObject slot;
		public Piece? piece;
		private GameObject displayCube; 

		public Tile(GameObject slot, Piece? piece) {
			this.slot = slot;
			this.piece = (piece == null)? piece : null;

			this.displayCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			this.displayCube.transform.SetParent(slot.transform, false);
			this.displayCube.transform.position = new Vector3(
				displayCube.transform.position.x,
				displayCube.transform.position.y + 1,
				displayCube.transform.position.z);
			this.displayCube.transform.localScale = new Vector3(3, 1, 3);
			this.displayCube.SetActive(false);
		}

		public void setPiece(Piece piece) {
			piece.transform.SetParent(slot.transform, false);
		}

		public void enableDisplay(Player player) {
            if (piece != null) {
                if (piece.Value.owner != player) {
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
	}
}