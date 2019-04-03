using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This code should be appplied to all objects not part of the tilemap.
 */

public class ForegroundSwitchableIndividual : MonoBehaviour {
	public GameController gameController;
	private SpriteRenderer[] sirRender;
	private Collider2D[] _colliders;
	public Color preferredColor;
	public enum ColorArea{Red,Green,Blue};
	public ColorArea myColorArea;

	private float preferred_red, preferred_green, preferred_blue, preferred_albedo; 
	Color s_combined;

	void Start (){
		_colliders = GetComponents<Collider2D>();
		preferred_red = preferredColor.r;
		preferred_green = preferredColor.g;
		preferred_blue = preferredColor.b;
		preferred_albedo = preferredColor.a;
		foreach (Collider2D collider in _colliders) {
			collider.enabled = false;
		}
		sirRender = GetComponentsInChildren<SpriteRenderer> ();
	}	

	/*
	 * The assumption I am making at this time is this is all one level, and 
	 * the player can only get one orb at a time, in sequential order.
	 */ 

	void solidify(){
		//Sets layer to foreground
		gameObject.layer = 8;
		//Shifts tile to foreground (not technically necessary under current implementation)
		transform.position = new Vector3(transform.position.x,transform.position.y,0);
		foreach (Collider2D collider in _colliders) {
			collider.enabled = true;
		}
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
		if (gameController.orbGetRed){
			//s_combined = new Color (preferred_red, s_combined.g, s_combined.b);
			if (myColorArea == ColorArea.Red) {
				solidify ();
			}
		}

		if (gameController.orbGetGreen) {
			//s_combined = new Color (preferred_red, preferred_green, 0f);
			if (myColorArea == ColorArea.Green) {
				solidify ();
			}
		}

		if (gameController.orbGetBlue) {
			//s_combined = preferredColor;
			if (myColorArea == ColorArea.Blue) {
				solidify ();
			}
		}

		foreach (SpriteRenderer s_render in sirRender) {
			s_render.color = determineColor ();
		}
	}
		
	void Update () {
		
	}
}
