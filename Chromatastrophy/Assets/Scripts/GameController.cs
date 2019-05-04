using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameController : MonoBehaviour {
	public static GameController controller;

	[HideInInspector]
	public Vector3 lastSavedLocation;

	[HideInInspector]
	public String lastRoomEntered;

	public bool orbGetRed = false;
	public bool orbGetGreen = false;
	public bool orbGetBlue = false;

	private ColorChangeProps[] ccp;

	private TilemapSwitchable[] tms;

	public Player _player;

	void Awake(){
	//This is a pho-singleton pattern which allows for only one game controller to exist		
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;

		} else if (controller != null) {
			Destroy (this);
		}
		ccp = UnityEngine.Object.FindObjectsOfType<ColorChangeProps> ();
		tms = UnityEngine.Object.FindObjectsOfType<TilemapSwitchable> ();

		_player = FindObjectOfType<Player> ();
	}

	void Start(){
		
		//adjustPlayerAbilities ();
		paintWorld ();
	}
	void adjustPlayerAbilities(){
		if (orbGetRed) {
			_player.canDoubleJump = true;
		} else {
			_player.canDoubleJump = false;
		}

		if (orbGetGreen) {
			_player.wallJumpOff = new Vector2 (20, 20);
			_player.wallLeap = new Vector2 (20, 20);
			_player.wallStickTime = 0.25f;
			//Slight wall speed for stair issues
			_player.wallSlideSpeedMax = 1;
		} else {
			_player.wallJumpOff = new Vector2 (0, 0);
			_player.wallLeap = new Vector2 (0, 0);
			_player.wallStickTime = 0;
			_player.wallSlideSpeedMax = 100;
		}

		if (orbGetBlue) {
			//Water stuff
		} else {
			//Water stuff
		}


	}
	public void paintWorld(){
		
		adjustPlayerAbilities ();
		foreach (ColorChangeProps prop in ccp) {
			prop.colorize ();
			Debug.Log ("colorized");
			if (prop.isObstructive) {
				if (orbGetRed && prop.colorArea == ColorChangeProps.ColorArea.Red) {
					prop.solidify ();
				} else if (orbGetGreen && prop.colorArea == ColorChangeProps.ColorArea.Green) {
					prop.solidify ();
				} else if (orbGetBlue && prop.colorArea == ColorChangeProps.ColorArea.Blue) {
					prop.solidify ();
				} else {
					prop.liquify ();
				}
			}
		}
			

		foreach (TilemapSwitchable tilemap in tms) {
			tilemap.colorize ();
		}
	}
	// Update is called once per frame
	void Update () {
		if (_player == null) {
			_player = FindObjectOfType<Player> ();
		}
	}

	public void Save(){
		lastSavedLocation = _player.transform.position;

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
	
		GameData data = new GameData ();
		data.orbGetRed = orbGetRed;
		data.orbGetGreen = orbGetGreen;
		data.orbGetBlue = orbGetBlue;
		data.playerX = lastSavedLocation.x;
		data.playerY = lastSavedLocation.y;
		data.lastRoomEntered = lastRoomEntered;
		bf.Serialize (file, data);
		file.Close ();

		Debug.Log (lastSavedLocation.ToString ());
		Debug.Log ("Saved");
	}

	public void Load (){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

			GameData data = (GameData)bf.Deserialize (file);
			orbGetRed = data.orbGetRed;
			orbGetGreen = data.orbGetGreen;
			orbGetBlue = data.orbGetBlue;
			lastSavedLocation = new Vector3 (data.playerX, data.playerY,0f);
			lastRoomEntered = data.lastRoomEntered;

			if(lastRoomEntered != SceneManager.GetActiveScene().name){
				SceneManager.LoadScene (lastRoomEntered, LoadSceneMode.Single);
			}
			_player = FindObjectOfType<Player> ();
			//_player.transform.position = lastSavedLocation;
		}

		//Debug.Log (lastSavedLocation.ToString () + ", " + _player.transform.position.ToString ());
		Debug.Log ("Loaded");
	}
		
}

[Serializable]
class GameData{
	public bool orbGetRed;
	public bool orbGetGreen;
	public bool orbGetBlue;
	public float playerX;
	public float playerY;
	public String lastRoomEntered;
}
