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
	public enum ColorArea{Red,Green,Blue};
	public ColorArea myColorArea;
	public Color preferredColor;
	private float preferred_red, preferred_green, preferred_blue; 
	private Color s_combined;

	//Something should change the mask to foreground in code

	void Start (){
		outline = GetComponent<TilemapCollider2D> ();
		outline.enabled = false;
		_tilemap = GetComponent<Tilemap> ();
		preferred_red = preferredColor.r;
		preferred_green = preferredColor.g;
		preferred_blue = preferredColor.b;
	}	

	public void colorize(){

			if (gameController.orbGetRed){
				s_combined = new Color (preferred_red, 0f, 0f);
				if (myColorArea == ColorArea.Red) {
					solidify ();
				}
			}

			if (gameController.orbGetGreen) {
				s_combined = new Color (preferred_red, preferred_green, 0f);
				if (myColorArea == ColorArea.Green) {
					solidify ();
				}
			}

			if (gameController.orbGetBlue) {
				s_combined = preferredColor;
				if (myColorArea == ColorArea.Blue) {
					solidify ();
				}
			}

		_tilemap.color = s_combined;
	}

	void solidify(){
		Debug.Log ("Solidify");
		//Note that 8 is the current layer of foreground, should change if this changes
		int foreground = 8;
		gameObject.layer = foreground;
		outline.enabled = true;
	}
	void Update () {
			
			//This foreach credt goes to Unity forums user 'keely'
			//This will be used if we want to change the sprites themselves instead of just color
			//foreach (Vector3Int position in _tilemap.cellBounds.allPositionsWithin) {
				//changeling = _tilemap.GetSprite (position);
				//changeName = (changeling.name +"_Switched");
	}
}
