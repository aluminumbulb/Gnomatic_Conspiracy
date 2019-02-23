using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	bool facingLeft = true;
	Transform player;
	Vector3 currentPosition;
	float hSpeed;
	public float walkSpeed = 10f;
	// Use this for initialization
	void Start () {
		player = GetComponent<Transform> ();	
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		currentPosition = player.localPosition;
	}

	void Update () {
		hSpeed = Input.GetAxis ("Horizontal") * walkSpeed;

		if (facingLeft && (hSpeed > 0)) {
			Flip ();
		}

		if (!facingLeft && (hSpeed < 0)) {
			Flip ();
		}

	}

	//Flips transform and thereby all children.
	void Flip(){
		facingLeft = !facingLeft;
		player.RotateAround (currentPosition, Vector3.up, 180f);
	}
}
