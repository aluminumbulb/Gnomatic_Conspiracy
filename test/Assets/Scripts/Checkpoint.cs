using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// When this object collides with an object with the CharacterMovement
	// script, calls their updateCheckpoint() function with this game object.
	void OnCollisionEnter2d(Collider2D other) {
		CharacterMovement player = other.GetComponent<CharacterMovement> ();
		if (player != null) {
			Debug.Log ("Touched the Checkpoint");
			player.updateCheckpoint (this.gameObject);
		}
	}
}
