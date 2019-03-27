using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Adding filler text to force github to see a change
public class GameController : MonoBehaviour {
	public bool orbGetRed = false;
	public bool orbGetGreen = false;
	public bool orbGetBlue = false;

	private ForegroundSwitchableIndividual[] chO;
	private TilemapSwitchable[] tmO;


	// Use this for initialization
	void Start () {
			
	}

	public void paintWorld(){
		chO = Object.FindObjectsOfType<ForegroundSwitchableIndividual> ();
		foreach (ForegroundSwitchableIndividual individual in chO) {
			individual.colorize ();
		}

		tmO = Object.FindObjectsOfType<TilemapSwitchable> ();
		foreach (TilemapSwitchable tilemap in tmO) {
			tilemap.colorize ();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
		
}
