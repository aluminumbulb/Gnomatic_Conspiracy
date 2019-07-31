using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SavePoint : MonoBehaviour {
	[HideInInspector]
	private Vector3 mySaveLocation;
	GameController _gameController;
	// Use this for initialization
	void Start () {
		mySaveLocation = transform.position;
		_gameController = GameObject.FindObjectOfType<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D surrounding = Physics2D.CircleCast(transform.position,0.3f,new Vector2(0,0));
		if (surrounding.collider != null) {
			if (surrounding.collider.tag == "Player") {
				_gameController.Save(mySaveLocation, SceneManager.GetActiveScene ().name);
			}
		}
	}
}
