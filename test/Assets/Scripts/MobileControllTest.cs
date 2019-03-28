using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MobileControllTest : MonoBehaviour {
	bool leftSideTouched = false;
	bool rightSideTouched = false;
	Rect leftSide,rightSide;

	public Text responeseText;

	SpriteRenderer sRend;
	// Use this for initialization
	void Start () {
		sRend = GetComponent<SpriteRenderer> ();
		responeseText.text = "";
		leftSide = new  Rect(0, 0, Screen.width / 4.0f, Screen.height);
		rightSide = new Rect(((Screen.width/4f)*3),0,Screen.width / 4.0f,Screen.height);
	}

	// Update is called once per frame
	void Update () {
		mobileControlls ();
	}

	private void mobileControlls(){
		if (Input.touchCount > 0) {
			responeseText.text = "Touch Encountered";
			sRend.color = Color.black;
			Vector2[] touchPlaces = new Vector2[Input.touches.Length];
			for (int i = 0; i < touchPlaces.Length; i++){
				responeseText.text = touchPlaces [i].ToString();
				touchPlaces [i] = Input.touches [i].position;
			}

			for (int j = 0; j < touchPlaces.Length; j++) {
				if (leftSide.Contains (touchPlaces [j])) {
					leftSideTouched = true;
				}

				if (rightSide.Contains (touchPlaces [j])) {
					rightSideTouched = true;
				}
			}

			if (rightSideTouched && !leftSideTouched) {
				sRend.color = Color.magenta;
			}

			if (!rightSideTouched && leftSideTouched) {
				sRend.color = Color.cyan;
			}

			if (rightSideTouched && leftSideTouched) {
				sRend.color = Color.green;
			}
		}
		leftSideTouched = false;
		rightSideTouched = false;
	}
}
