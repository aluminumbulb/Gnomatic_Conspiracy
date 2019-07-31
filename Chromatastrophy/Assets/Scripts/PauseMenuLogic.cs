using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuLogic : MonoBehaviour {
	GameController _gameController;
	Player _player;

	void Awake () {
		_gameController = GameObject.FindObjectOfType <GameController>();
		_player = GameObject.FindObjectOfType<Player> ();
	}


	public void Save (){
		_gameController.Save (_player.transform.position, SceneManager.GetActiveScene().name);
	}

	public void Load(){
		_gameController.Load ();
	}
}
