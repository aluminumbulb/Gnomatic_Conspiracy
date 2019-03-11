using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ColorSwitchTiles : Tile {
	public Sprite _sprite;
	public float s_red = 255.0f, s_green = 255.0f, s_blue = 255.0f; 
	Color s_combined;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		s_combined = new Color (s_red, s_green, s_blue);
		color = s_combined;
	}
}
