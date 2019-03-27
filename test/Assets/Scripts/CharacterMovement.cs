using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	bool facingLeft = true;
	Transform player;
	Rigidbody2D playersRigidbody;
	Vector3 currentPosition;
	Vector2 lockPosition;
	GameObject lastCheckpoint;
	bool locked = false;


	//Jumping variables
	public bool jumping;

	public float jumpForce = 200.0f;


	float hSpeed;
	public float walkSpeed = 100f;

	//Touchscreen Variables
	private float leftSideEnd;
	private float rightSideStart;

	// Use this for initialization
	void Start () {
		leftSideEnd = Screen.width / 4.0f;
		rightSideStart = 3 * (Screen.width) / 4.0f;
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

		WallCling ();

		//Mobile Controlls:

		if (Input.touchCount > 1) {
			if (Input.GetTouch (0).position.x < leftSideEnd && Input.GetTouch (1).position.x > rightSideStart) {
				if (!jumping) {
					playersRigidbody.AddForce (Vector2.up * jumpForce);
					jumping = true;	
				}
			} else if (Input.GetTouch (1).position.x < leftSideEnd && Input.GetTouch (0).position.x > rightSideStart) {
				if (!jumping) {
					playersRigidbody.AddForce (Vector2.up * jumpForce);
					jumping = true;	
				}
			}
		} else {
			Touch playerTouch = Input.GetTouch (0);
			if (playerTouch.position.x < leftSideEnd) {
				playersRigidbody.AddForce (Vector2.left * walkSpeed * Time.deltaTime);
			}

			if (playerTouch.position.x > rightSideStart) {
				playersRigidbody.AddForce (Vector2.right * walkSpeed * Time.deltaTime);
			}
		}



		
	}

	void Flip(){
		facingLeft = !facingLeft;
		player.RotateAround (currentPosition, Vector3.up, 180f);
	}

	void OnCollisionEnter2D(Collision2D other){
		jumping = false;
	}

	void OnCollisionExit2D(Collision2D other){
		jumping = true;
	}

	void WallCling(){
		LayerMask clingable = LayerMask.GetMask ("Foreground");
		RaycastHit2D grounded =  Physics2D.Raycast (transform.position, Vector2.down, 2.5f, clingable);
		if (grounded.collider) {
			//Debug.Log ("Touching Ground");
		}
		RaycastHit2D wallRight = Physics2D.Raycast (transform.position, Vector2.right, .75f, clingable);
		RaycastHit2D wallLeft = Physics2D.Raycast (transform.position, Vector2.right, .75f, clingable);
		if ((wallRight.collider != null && grounded.collider == null) || (wallLeft.collider != null && grounded.collider == null)) {

				Debug.Log ("Can Cling");
			if (!locked) {
				lockPosition = transform.position;
			}
			if(Input.GetKeyDown (KeyCode.Space)) {
				locked = true;
					jumping = true;
					transform.position = lockPosition;
				}

				if (Input.GetKeyUp (KeyCode.Space)) {
					jumping = false;
					locked = false;
					playersRigidbody.AddForce (new Vector2 (-1f, 1f), ForceMode2D.Impulse);
				}
		}
	}

	// Records the last checkpoint touched. Called by the Checkpoint script.
	public void updateCheckpoint(GameObject other) {
		lastCheckpoint = other;
	}

	/**
	 * If called, teleports the player to the last checkpoint touched.
	 * If the player hasn't touched a checkpoint, then it triggers a jump.
	 * Not finalized yet.
	 * */
	public void hasBeenHurt() {
		if (lastCheckpoint != null) {
			transform.position = lastCheckpoint.transform.position;
			transform.rotation = lastCheckpoint.transform.rotation;
		} else {
			playersRigidbody.AddForce (Vector2.up * jumpForce);
		}
	}
}
