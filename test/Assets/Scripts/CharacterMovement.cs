using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	bool facingLeft = true;
	Transform player;
	Rigidbody2D playersRigidbody;
	Vector3 currentPosition;
	GameObject lastCheckpoint;

	//Jumping variables
	public bool jumping;
	public float jumpForce = 100.0f;


	float hSpeed;
	public float walkSpeed = 10f;


	// Use this for initialization
	void Start () {
		player = GetComponent<Transform> ();
		playersRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		currentPosition = player.localPosition;
	}

	void Update () {
		hSpeed = Input.GetAxis ("Horizontal") * walkSpeed;

		playersRigidbody.AddForce (Vector2.right * hSpeed);

		if (facingLeft && (hSpeed > 0)) {
			Flip ();
		}

		if (!facingLeft && (hSpeed < 0)) {
			Flip ();
		}
	
		//jumping controls, Travis
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.Space)) {
				if (!jumping) {
					playersRigidbody.AddForce (Vector2.up * jumpForce);
					jumping = true;	
				}	
		}
	}


	public void updateCheckpoint(GameObject other) {
		lastCheckpoint = other;
	}

	public void hasBeenHurt() {
		if (lastCheckpoint != null) {
			transform.position = lastCheckpoint.transform.position;
			transform.rotation = lastCheckpoint.transform.rotation;
		} else {
			playersRigidbody.AddForce (Vector2.up * 14);
		}
	}

	/** void OnTriggerEnter2d(Collider2D other) {
		// Saves the last checkpoint you touched.
		Checkpoint respawn = other.GetComponent<Checkpoint> ();
		if (respawn != null) {
			lastCheckpoint = other.gameObject;
		}

		// If you get hurt, go to the checkpoint.
		PainfulObject ouch = other.GetComponent<PainfulObject> ();
		if (ouch != null) {
			if (lastCheckpoint != null) {
				transform.position = lastCheckpoint.transform.position;
				transform.rotation = lastCheckpoint.transform.rotation;
			} else {
				playersRigidbody.AddForce (Vector2.up * 14);
			}
		}
	} */

	//Flips transform and thereby all children.
	void Flip(){
		facingLeft = !facingLeft;
		player.RotateAround (currentPosition, Vector3.up, 180f);
	}
}
