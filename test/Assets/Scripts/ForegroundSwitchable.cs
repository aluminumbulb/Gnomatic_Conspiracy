using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundSwitchable : MonoBehaviour {
	//public bool isVisible = false;
	public GameController gameController;
	private BoxCollider2D outline;
	private SpriteRenderer sirRender;
	public float s_red = 255.0f, s_green = 255.0f, s_blue = 255.0f; 
	Color s_combined;
	// Use this for initialization
	void Start (){
		outline = GetComponent<BoxCollider2D> ();
		sirRender = GetComponent<SpriteRenderer> ();
	}	
	
	// Update is called once per frame
	void Update () {
		s_combined = new Color (s_red, s_green, s_blue);
		outline.enabled = gameController.isVisible;
		if (gameController.isVisible) {
			sirRender.color = s_combined;
		}

	}
}
