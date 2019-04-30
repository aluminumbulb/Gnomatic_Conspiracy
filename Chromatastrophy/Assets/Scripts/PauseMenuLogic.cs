using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuLogic : MonoBehaviour {
	GameController _gameController;
	Player _player;
	// Use this for initialization

	void Start () {
		_gameController = GameObject.FindObjectOfType <GameController>();
	}

	public void Save (){
		_gameController.Save ();
	}

	public void Load(){
		_gameController.Load ();
	}
}
