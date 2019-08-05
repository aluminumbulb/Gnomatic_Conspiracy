using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

	private Collider2D playerCollider;
	Rect leftRect,rightRect, jumpRect;
	public int dashSensitivity = 10;
    
	private void Start()
    {
        player = GetComponent<Player>();

		leftRect = new  Rect(0, 0, Screen.width / 5.0f, Screen.height);
		rightRect = new Rect((Screen.width/5f),0, Screen.width / 5.0f,Screen.height);
		jumpRect = 	new Rect((Screen.width*(3f/5f)),0, Screen.width*3 / 5.0f,Screen.height);
		playerCollider = GetComponent<BoxCollider2D> ();
	}

    private void Update()
    {
		if (!player.paused) {
			Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			player.SetDirectionalInput (directionalInput);

			if (Input.GetKeyDown (KeyCode.Space)) {
				player.onDash ();
			}

			if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow))) {
				player.OnJumpInputDown ();
			}

			if ((Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow))) {
				player.OnJumpInputUp ();
			}
			if (Input.touches.Length > 0) {
				mobileControlls (Input.touches);
			}
		}
    }

	private void mobileControlls(Touch[] touches){
		foreach (Touch touch in touches) {
			//Checks for left hand motion
			if (leftRect.Contains (touch.position)) {
				player.SetDirectionalInput (Vector2.left);
			}

			//Check for right hand motion
			if (rightRect.Contains (touch.position)) {
				player.SetDirectionalInput (Vector2.right);
			}

			//Checks for jump behavor
			if (jumpRect.Contains (touch.position)) {
				if (touch.phase == TouchPhase.Began) {
					player.OnJumpInputDown ();
				}

				if (touch.phase == TouchPhase.Ended) {
					player.OnJumpInputUp ();
				}
			}

			//Checks for any draggings
			if (Mathf.Abs(touch.deltaPosition.x) > dashSensitivity) {
				player.onDash ();
			}
		}
	
	}
}


