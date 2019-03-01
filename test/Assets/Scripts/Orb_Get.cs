using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_Get : MonoBehaviour {
	private CircleCollider2D orb;
	public GameController gameController;
	// Use this for initialization
	void Start () {
		orb = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 10, 0);
	}

	void OnTriggerEnter2D(Collider2D other){
		CharacterMovement player = other.GetComponent<CharacterMovement> ();
		if (player != null) {
			Debug.Log ("Touched Sphere");
			gameController.isVisible = true;
			Destroy (this.gameObject);
		}
	}
}
