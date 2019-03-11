using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This is the presently functional color switch code
 * it only works on single elements, which might be 
 * a choke point of later prodction, so I'm working on
 * TileMap Switchable to affect whole maps
 */

public class ForegroundSwitchableIndividual : MonoBehaviour {
	//public bool isVisible = false;
	public GameController gameController;
	private SpriteRenderer sirRender;
	private BoxCollider2D outline;
	public float s_red = 255.0f, s_green = 255.0f, s_blue = 255.0f; 
	Color s_combined;
	void Start (){
		outline = GetComponent<BoxCollider2D> ();
		sirRender = GetComponent<SpriteRenderer> ();
	}	

	//This should be switched out with a code that is called only once9
	void Update () {
		s_combined = new Color (s_red, s_green, s_blue);
		outline.enabled = gameController.isVisible;
		if (gameController.isVisible) {
			sirRender.color = s_combined;
		}
	}
}
