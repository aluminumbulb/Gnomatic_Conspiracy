using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MobileControllTest : MonoBehaviour {
	Rect leftSide,rightSide;
	int rightTap, leftTap;
	bool touchingLeft = false, touchingRight = false;
	Touch leftTouch, rightTouch;
	Vector2 leftStartPos, rightStartPos;
	List<Touch> leftTouches = new List<Touch> ();
	List<Touch> rightTouches = new List<Touch> ();
	SpriteRenderer sRend;
	// Use this for initialization
	void Start () {
		sRend = GetComponent<SpriteRenderer> ();
		leftSide = new  Rect(0, 0, Screen.width / 4.0f, Screen.height);
		rightSide = new Rect(((Screen.width/4f)*3),0,Screen.width / 4.0f,Screen.height);
	}

	// Update is called once per frame
	void Update () {
		mobileControlls ();
	}

	private void mobileControlls(){
		touchingLeft = false;
		touchingRight = false;
		foreach (Touch touch in Input.touches) {
			if (leftSide.Contains (touch.position)) {
				leftTouch = touch;
				leftStartPos = leftTouch.position;
				touchingLeft = true;
			}

			if (rightSide.Contains (touch.position)) {
				rightTouch = touch;
				rightStartPos = rightTouch.position;
				touchingRight = true;
			}
		}
			
		if (touchingLeft && touchingRight) {
			Debug.Log ("jump");
		}

		if (touchingLeft && !touchingRight) {
			if (leftTouch.phase == TouchPhase.Stationary) {
				Debug.Log("Move left");
			} 

			if (leftTouch.phase == TouchPhase.Moved) {
					Debug.Log ("Dash Left");
			}
		}


		if (!touchingLeft && touchingRight) {
			if (rightTouch.phase == TouchPhase.Stationary) {
				Debug.Log("Move Right");
			} 

			if (rightTouch.phase == TouchPhase.Moved) {
				Debug.Log ("Dash Right");
			}
		}
	}
}
				

