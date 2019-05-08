using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeSpikes : MonoBehaviour {

	// public GameController _gameController;
	public GameObject nextSpike;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}

	void OnEnable() {
		if (nextSpike) {
			nextSpike.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
