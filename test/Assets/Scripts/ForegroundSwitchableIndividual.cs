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

	private float preferred_red, preferred_green, preferred_blue; 
	Color s_combined;

	/*
	 * The way this bit is coded, all switchable objects MUST have
	 * a composite collider
	 */

	void Start (){
		_colliders = GetComponents<Collider2D>();
		preferred_red = preferredColor.r;
		preferred_green = preferredColor.g;
		preferred_blue = preferredColor.b;
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
		foreach (Collider2D collider in _colliders) {
			collider.enabled = true;
		}
		gameObject.layer = 8;
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

		foreach (SpriteRenderer s_render in sirRender) {
			s_render.color = s_combined;
		}
	}
		
	void Update () {
		
	}
}
