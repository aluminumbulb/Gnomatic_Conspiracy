using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//Adding filler text to force github to see a change
public class GameController : MonoBehaviour {
	public static GameController controller;

	public bool orbGetRed = false;
	public bool orbGetGreen = false;
	public bool orbGetBlue = false;

	private ForegroundSwitchableIndividual[] fsi;
	private TilemapSwitchable[] tms;

	public Player _player;
	// Use this for initialization

	void Awake(){
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;

		} else if (controller != null) {
			Destroy (this);
		}
	}

	void Start () {
		
	}

	void adjustPlayerAbilities(){
		if (orbGetRed) {
			_player.canDoubleJump = true;
		} else {
			_player.canDoubleJump = false;
		}

		if (orbGetGreen) {
			//Dash on
		} else {
			//Dash off
		}

		if (orbGetBlue) {
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
	}
	public void paintWorld(){
		adjustPlayerAbilities ();
		fsi = UnityEngine.Object.FindObjectsOfType<ForegroundSwitchableIndividual> ();
		foreach (ForegroundSwitchableIndividual individual in fsi) {
			individual.colorize ();
		}

		tms = UnityEngine.Object.FindObjectsOfType<TilemapSwitchable> ();
		foreach (TilemapSwitchable tilemap in tms) {
			tilemap.colorize ();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
	
		GameData data = new GameData ();
		data.orbGetRed = orbGetRed;
		data.orbGetGreen = orbGetGreen;
		data.orbGetBlue = orbGetBlue;

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
		}
		Debug.Log ("Loaded");
	}
		
}

[Serializable]
class GameData{
	public bool orbGetRed;
	public bool orbGetGreen;
	public bool orbGetBlue;
}
