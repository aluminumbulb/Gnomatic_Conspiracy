using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_Get : MonoBehaviour {
	private CircleCollider2D orb;
	public GameController gameController;

	public enum orbType{Red, Green, Blue};
	public orbType myOrbType;

	// Use this for initialization
	void Start () {
		orb = GetComponent<CircleCollider2D> ();
		//myOrbType = orbType.Red;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		CharacterMovement player = other.GetComponent<CharacterMovement> ();
		if (player != null) {
			if(myOrbType == orbType.Red){
				gameController.orbGetRed = true;
			}
			if (myOrbType == orbType.Green) {
				gameController.orbGetGreen = true;
			}
			if (myOrbType == orbType.Blue) {
				gameController.orbGetBlue = true;
			}

			gameController.paintWorld ();
			Destroy (this.gameObject);
		}
	}
}
