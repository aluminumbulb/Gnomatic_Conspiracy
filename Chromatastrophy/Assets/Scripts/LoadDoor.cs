using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDoor : MonoBehaviour {
	SpriteRenderer _spriteRenderer;
	GameController _gameController;

	public Sprite openDoor;
	public Sprite closedDoor;

	void Start () {
		_gameController = GameObject.FindObjectOfType<GameController> ();
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		RaycastHit2D doorFrame = Physics2D.BoxCast (new Vector2(transform.position.x, transform.position.y), new Vector2 (2f, 4f), 0, Vector2.zero);
		if (doorFrame.collider != null) {
			if (doorFrame.collider.tag == "Player") {
				_spriteRenderer.sprite = openDoor; 
				if (Input.GetKeyDown(KeyCode.E)) {
					_gameController.Load ();
				}
			}
		}else if (_spriteRenderer.sprite != closedDoor) {
			_spriteRenderer.sprite = closedDoor;
		}
	}
}
