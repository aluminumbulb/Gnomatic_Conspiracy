using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainfulObject : MonoBehaviour {

	// Currently just a flag. Next update:
	// Add collision detection to call hasBeenHurt() on all objects with CharacterMovement.

	void onCollisionEnter2D(Collider2D Other) {
		CharacterMovement player = Other.GetComponent<CharacterMovement> ();
		if (player != null) {
			Debug.Log ("Ouch!");
			player.hasBeenHurt ();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
