using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSwitchable2 : MonoBehaviour {
	public GameController _gameController;
	private Tilemap _tilemap;
	private TilemapCollider2D outline;
	private TilemapRenderer _tRender;	
	TileBase[] _tiles;
	private List<Sprite> sprites = new List<Sprite>();
	private Sprite copySprite;
	private Sprite originalSprite;
	private int spriteWidth;
	private int spriteHeight;

	public enum ColorArea{Red,Green,Blue};
	public ColorArea myColorArea;

	// Use this for initialization
	void Start () {
		//outline = GetComponent<TilemapCollider2D> ();
		//outline.enabled = false;
		_tilemap = GetComponent<Tilemap> ();

	}

	public void colorize(){
		Debug.Log ("attempting to colorize");
		foreach (Vector3Int position in _tilemap.cellBounds.allPositionsWithin) {
			originalSprite = _tilemap.GetSprite (position);
			if (originalSprite != null) {
				copySprite = Instantiate(originalSprite);
				spriteWidth = copySprite.texture.width;
				spriteHeight = copySprite.texture.height;
				for (int i = 0; i < spriteWidth; i++) {
					for (int j = 0; j < spriteHeight; j++) {
						//Scans the original sprite for information on what the copy should do
						Color recolor = findAppropriateColor (originalSprite.texture.GetPixel (i, j));	
						copySprite.texture.SetPixel (i, j, recolor);
					}
				}

				copySprite.texture.Apply ();
				Tile putMeIn = new Tile ();
				putMeIn.sprite = copySprite;
				_tilemap.SetTile (position, putMeIn);
			}
		}
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

	// Update is called once per frame
	void Update () {
		
	}
}
