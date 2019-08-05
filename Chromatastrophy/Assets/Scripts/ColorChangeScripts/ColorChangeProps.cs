using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script affects all color change objects,
 * should be added to every object. If the object
 * is non-obstructive no collider will be needed
 */

public class ColorChangeProps : MonoBehaviour {
	public GameController _gameController;

	//variables involving the copying of sprites
	private SpriteRenderer sir_render;
	public Sprite originalSprite;
	Rect originalSpriteBounds;
	private Texture2D copyTexture;
	private int spriteWidth;
	private int spriteHeight;
	private Sprite copySprite;

	public bool isObstructive = false;
	private Collider2D _collider;
	public enum ColorArea{Red,Green,Blue};
	public ColorArea colorArea;


	void Awake(){
		sir_render = GetComponent<SpriteRenderer> ();
		spriteWidth = originalSprite.texture.width;
		spriteHeight = originalSprite.texture.height;
		//Creates a replica sprite based on supplied texture
		copyTexture = Instantiate (originalSprite.texture);
		originalSpriteBounds = new Rect (0, 0, spriteWidth, spriteHeight);
		copySprite = Sprite.Create(copyTexture, originalSpriteBounds, new Vector2(0.5f, 0.5f));	
		//Sets replica sprite as sprite renderer's focus
		sir_render.sprite = copySprite;

		gameObject.layer = 10;
		if (isObstructive) {
			//_collider = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
			_collider = gameObject.GetComponent<Collider2D> ();
			_collider.enabled = false;
		}
	}

	void Start () {

	}

	void Update () {
		//sir_render.color = prefColor;
		if (Input.GetKeyDown (KeyCode.S)) {
			_gameController.paintWorld ();
		}
	}
		
	public void colorize(){
		for (int i = 0; i < spriteWidth; i++) {
			for (int j = 0; j < spriteHeight; j++) {
				//Scans the original sprite for information on what the copy should do
				Color recolor = findAppropriateColor(originalSprite.texture.GetPixel (i, j));	
				copyTexture.SetPixel (i, j, recolor);
			}
		}
		copyTexture.Apply ();
	}

	private Color findAppropriateColor(Color colorIn){
		float redValue = colorIn.r;
		float greenValue = colorIn.g;
		float blueValue = colorIn.b;
		float greyValue = (colorIn.r+colorIn.g+colorIn.b)/3;

		Color finalColor = new Color(greyValue, greyValue, greyValue);
		if (_gameController.orbGetRed) {
			finalColor.r = redValue;
		}

		if(_gameController.orbGetGreen){
			finalColor.g = greenValue;
		}

		if(_gameController.orbGetBlue){
			finalColor.b = blueValue;
		}

		return finalColor;
	}


	public void solidify(){
		Debug.Log ("solidify");
		gameObject.layer = 8;
		_collider.enabled = true;
	}

	public void liquify(){
		Debug.Log ("liquify");
		gameObject.layer = 10;
		_collider.enabled = false; 
	}
}
