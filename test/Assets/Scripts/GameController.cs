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

	// Use this for initialization
	void Start () {
			
	}

	public void paintWorld(){
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
