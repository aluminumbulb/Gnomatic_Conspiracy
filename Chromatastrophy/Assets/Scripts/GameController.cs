using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameController : MonoBehaviour {
	public static GameController controller;

	public AudioSource audioSource;

	public AudioClip bg1, bg2, bg3, bg4;

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
	}

	void manageMusic(){
		if (!audioSource.isPlaying) {
			audioSource.loop = true;
			if (!orbGetRed)
				audioSource.volume = .80f;
				audioSource.clip = bg1;
				audioSource.Play ();

			if (orbGetRed && !(orbGetGreen) && !(orbGetBlue))
				audioSource.clip = bg2;
				audioSource.volume = .82f;
				audioSource.Play ();

			if (orbGetGreen && !(orbGetBlue))
				audioSource.clip = bg3;
				audioSource.volume = .85f;
				audioSource.Play ();

			if (orbGetBlue)
				audioSource.clip = bg4;
				audioSource.volume = .86f;
				audioSource.Play ();
		}
	}

	void Start(){
		adjustPlayerAbilities ();
		paintWorld ();

		audioSource = GetComponent<AudioSource> ();
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
		manageMusic ();
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
			lastSavedLocation = new Vector3 (data.playerX, data.playerY);
			lastRoomEntered = data.lastRoomEntered;

			if(lastRoomEntered != SceneManager.GetActiveScene().name){
				SceneManager.LoadScene (lastRoomEntered, LoadSceneMode.Single);
				Instantiate(_player);
			}
			_player.transform.position = lastSavedLocation;
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
	public String lastRoomEntered;
}
