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

	Color s_original = new Color(1,1,1);

	void Start (){
		outline = GetComponent<TilemapCollider2D> ();
		outline.enabled = false;
		_tilemap = GetComponent<Tilemap> ();
		_tilemap.color = Color.gray;
	}	
		void Update () {
			if (gameController.isVisible) {
			_tilemap.color = s_original;
			outline.enabled = true;
			//This foreach credt goes to Unity forums user 'keely'
			//This will be used if we want to change the sprites themselves instead of just color
			//foreach (Vector3Int position in _tilemap.cellBounds.allPositionsWithin) {
				//changeling = _tilemap.GetSprite (position);
				//changeName = (changeling.name +"_Switched");
			}
			gameController.isVisible = false;
	}
}
