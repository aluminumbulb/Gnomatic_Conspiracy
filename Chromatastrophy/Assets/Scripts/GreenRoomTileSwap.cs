using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GreenRoomTileSwap : MonoBehaviour {
	public GameController gameController;
	private Tilemap _tilemap;
	private TilemapCollider2D outline;
	private Sprite changeling;
	private string changeName; 
	public Color preferredColor;
	private float preferred_red, preferred_green, preferred_blue;

	public enum ColorArea{Red,Green,Blue};
	public ColorArea myColorArea;
	private Color s_combined;

	void Awake(){
		gameController = FindObjectOfType<GameController> ();
		outline = GetComponent<TilemapCollider2D> ();
		outline.enabled = false;
		_tilemap = GetComponent<Tilemap> ();
	}

	void Start (){


		preferred_red = preferredColor.r;
		preferred_green = preferredColor.g;
		preferred_blue = preferredColor.b;
	}	

	private Color determineColor(){
		if (gameController.orbGetRed) {
			s_combined.r = preferred_red;
		} else {
			s_combined.r = 0f;
		}

		if (gameController.orbGetGreen) {
			s_combined.g = preferred_green;
		} else {
			s_combined.g = 0f;
		}

		if (gameController.orbGetBlue) {
			s_combined.b = preferred_blue;
		} else {
			s_combined.b = 0f;
		}

		s_combined.a = 255f;

		return s_combined;
	}
	public void colorize(){

		if (myColorArea == ColorArea.Green) {
			if (gameController.orbGetGreen) {
				gameObject.layer = 9;
				transform.position = new Vector3 (transform.position.x, transform.position.y, 9);
			} else {
				gameObject.layer = 11;
				transform.position = new Vector3 (transform.position.x, transform.position.y, 11);
			}
		}
	}


	void solidify(){
		gameObject.layer = 8;
		transform.position = new Vector3(transform.position.x,transform.position.y,0);
		outline.enabled = true;
	}

	void liquify(){
		gameObject.layer = 10;
		transform.position = new Vector3(transform.position.x,transform.position.y,10);
		outline.enabled = false;
	}
}
