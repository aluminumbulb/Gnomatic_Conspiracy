using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuLogic : MonoBehaviour {
	GameController _gameController;
	Player _player;

	//As of now, the player must be rigged up in inspector to make resume work

	void Awake () {
		_gameController = GameObject.FindObjectOfType <GameController>();
	}

	public void Save (){
		_gameController.Save ();
	}

	public void Load(){
		_gameController.Load ();
	}
}
