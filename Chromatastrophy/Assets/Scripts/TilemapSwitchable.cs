using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilemapSwitchable : MonoBehaviour {
	public GameController gameController;
	private Tilemap _tilemap;
	private TilemapCollider2D outline;
	private Sprite changeling;
	private string changeName; 
	public Color preferredColor;
	private float preferred_red, preferred_green, preferred_blue, preferred_albedo;

	public enum ColorArea{Red,Green,Blue};
	public ColorArea myColorArea;
	private Color s_combined;

	void Awake(){
		gameController = FindObjectOfType<GameController> ();
		if(gameController == null){
			Debug.Log ("controller not found");
		}


		outline = GetComponent<TilemapCollider2D> ();
		outline.enabled = false;
		_tilemap = GetComponent<Tilemap> ();

		preferred_red = preferredColor.r;
		preferred_green = preferredColor.g;
		preferred_blue = preferredColor.b;
		preferred_albedo = preferredColor.a;
	}
		
	void Start (){

		outline = GetComponent<TilemapCollider2D> ();
		outline.enabled = false;
		_tilemap = GetComponent<Tilemap> ();

	}	
		
	private Color determineColor(){
		if (gameController.orbGetRed) {
			s_combined.r = preferred_red;
			s_combined.a = preferred_albedo;
		} else {
			s_combined.r = 0f;
			s_combined.a = 0f;
		}

		if (gameController.orbGetGreen) {
			s_combined.g = preferred_green;
			s_combined.a = preferred_albedo;
		} else {
			s_combined.g = 0f;
			s_combined.a = 0f;
		}

		if (gameController.orbGetBlue) {
			s_combined.b = preferred_blue;
			s_combined.a = preferred_albedo;
		} else {
			s_combined.b = 0f;
			s_combined.a = 0f;
		}

		s_combined.a = 255f;

		return s_combined;
	}

	public void colorize(){
		if (myColorArea == ColorArea.Red) {
			if (gameController.orbGetRed){
				solidify ();
			} else {
				liquify ();
			}
		}
			
		if (myColorArea == ColorArea.Green) {
			if (gameController.orbGetGreen) {
				solidify ();
			}else{
				liquify ();
			}
		}

		if (myColorArea == ColorArea.Blue) {
			if (gameController.orbGetBlue) {
				solidify ();
			}else{
				liquify ();
			}
		}
		_tilemap.color = determineColor();

	}

	void solidify(){
		gameObject.layer = 8;
		transform.position = new Vector3(transform.position.x,transform.position.y,8);
		outline.enabled = true;
	}

	void liquify(){
		gameObject.layer = 10;
		transform.position = new Vector3(transform.position.x,transform.position.y,10);
		outline.enabled = false;
	}

	void Update () {

			//This foreach credt goes to Unity forums user 'keely'
			//This will be used if we want to change the sprites themselves instead of just color
			//foreach (Vector3Int position in _tilemap.cellBounds.allPositionsWithin) {
				//changeling = _tilemap.GetSprite (position);
				//changeName = (changeling.name +"_Switched");
	}
}
