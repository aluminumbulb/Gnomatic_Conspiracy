using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doors : MonoBehaviour {
	public Sprite openDoor;
	public Sprite closedDoor;
	public string levelName;
	SpriteRenderer _spriteRenderer;
	// Use this for initialization
	void Start () {
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D doorFrame = Physics2D.BoxCast (new Vector2(transform.position.x, transform.position.y), new Vector2 (2f, 4f), 0, Vector2.zero);
		if (doorFrame.collider != null) {
			if (doorFrame.collider.tag == "Player") {
				_spriteRenderer.sprite = openDoor; 
				if (Input.GetKeyDown(KeyCode.E)) {
					SceneManager.LoadScene (levelName, LoadSceneMode.Single);
				}
			}
		}else if (_spriteRenderer.sprite != closedDoor) {
			_spriteRenderer.sprite = closedDoor;
		}
	}
}
