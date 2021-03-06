using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Adding filler text to force github to see a change
public class GameController : MonoBehaviour {
	public bool orbGetRed = false;
	public bool orbGetGreen = false;
	public bool orbGetBlue = false;

	private ForegroundSwitchableIndividual[] fsi;
	private TilemapSwitchable[] tms;

	public Player _player;
	// Use this for initialization
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
			_player.wallSlideSpeedMax = 3;
		} else {
			_player.wallJumpOff = new Vector2 (0, 0);
			_player.wallLeap = new Vector2 (0, 0);
			_player.wallStickTime = 0;
			_player.wallSlideSpeedMax = 100;
		}
	}
	public void paintWorld(){
		adjustPlayerAbilities ();
		fsi = Object.FindObjectsOfType<ForegroundSwitchableIndividual> ();
		foreach (ForegroundSwitchableIndividual individual in fsi) {
			individual.colorize ();
		}

		tms = Object.FindObjectsOfType<TilemapSwitchable> ();
		foreach (TilemapSwitchable tilemap in tms) {
			tilemap.colorize ();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
		
}
