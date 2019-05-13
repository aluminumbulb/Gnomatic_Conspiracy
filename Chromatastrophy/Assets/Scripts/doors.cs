using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doors : MonoBehaviour {
	public Sprite openDoor;
	public Sprite closedDoor;
	public string levelName;
	private GameController _gameController;
	public bool redReq,greenReq,blueReq;
	SpriteRenderer _spriteRenderer;
	// Use this for initialization
	void Start () {
		_gameController = GameObject.FindObjectOfType<GameController> ();
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update (){
		RaycastHit2D doorFrame = Physics2D.BoxCast (new Vector2 (transform.position.x, transform.position.y), new Vector2 (1f, 2f), 0, Vector2.zero);
		if (doorFrame.collider != null) {
			if (doorFrame.collider.tag == "Player") {
				if (checkRequirements ()) {
					_spriteRenderer.sprite = openDoor; 
					//if (Input.GetKeyDown(KeyCode.E)) {
					SceneManager.LoadScene (levelName, LoadSceneMode.Single);
					//}
				}
			}
		}else if (_spriteRenderer.sprite != closedDoor) {
				_spriteRenderer.sprite = closedDoor;
			}

	}

	//If something is required, and the game controller doesnt have it, return false
	bool checkRequirements(){
		
		if (redReq && !_gameController.orbGetRed) {
			return false;
		}
		if (greenReq && !_gameController.orbGetGreen) {
			return false;
		}
		if (blueReq && !_gameController.orbGetBlue) {
			return false;
		}

		return true;
	}
}
