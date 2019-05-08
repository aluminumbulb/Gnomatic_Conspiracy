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
	public String lastRoomSaved;

	public bool orbGetRed = false;
	public bool orbGetGreen = false;
	public bool orbGetBlue = false;

	private ColorChangeProps[] ccp;

	private TilemapSwitchable[] tms;

	public Player _player;

	void Awake(){
		lastSavedLocation = new Vector3 (0, -2, 0);
	//This is a pho-singleton pattern which allows for only one game controller to exist		
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;

		} else if (controller != null) {
			Destroy (this);
		}
			
	}

	void Start(){


	
		
		//adjustPlayerAbilities ();
		paintWorld ();
	}

	void adjustPlayerAbilities(){
		_player = FindObjectOfType<Player> ();
		if (_player == null) {
			Debug.Log ("player not found");
		}
		_player.canDoubleJump = orbGetRed;	
		_player.canWallSlide = orbGetGreen;
		_player.canDash = orbGetBlue;
	}

	public void paintWorld(){
		ccp = UnityEngine.Object.FindObjectsOfType<ColorChangeProps> ();
		tms = UnityEngine.Object.FindObjectsOfType<TilemapSwitchable> ();
		//adjustPlayerAbilities ();
		foreach (ColorChangeProps prop in ccp) {
			prop.colorize ();
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
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
	
		GameData data = new GameData ();
		data.orbGetRed = orbGetRed;
		data.orbGetGreen = orbGetGreen;
		data.orbGetBlue = orbGetBlue;
		data.playerX = lastSavedLocation.x;
		data.playerY = lastSavedLocation.y;
		data.lastRoomSaved = lastRoomSaved;
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
			lastRoomSaved = data.lastRoomSaved;

			if(lastRoomSaved != SceneManager.GetActiveScene().name){
				SceneManager.LoadScene (lastRoomSaved, LoadSceneMode.Single);
			}
			_player = FindObjectOfType<Player> ();
		}
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
	public String lastRoomSaved;
}
