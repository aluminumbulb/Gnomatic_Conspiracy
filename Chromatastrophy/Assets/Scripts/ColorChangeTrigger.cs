using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour {
	private CircleCollider2D orb;
	public GameController gameController;

	public bool Red_Shift = false, Green_Shift = false, Blue_Shift = false;


	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D surrounding = Physics2D.CircleCast(transform.position,1,new Vector2(0,0));
		if (surrounding.collider != null) {
			gotten ();
		}
	}

	void gotten(){
		if (Red_Shift) {
			gameController.orbGetRed = true;
		} else {
			gameController.orbGetRed = false;
		}

		if (Green_Shift) {
			gameController.orbGetGreen = true;
		}else{
			gameController.orbGetGreen = false;
		}

		if (Blue_Shift) {
			gameController.orbGetBlue = true;
		} else {
			gameController.orbGetBlue = false;
		}
			gameController.paintWorld ();
			//Destroy (this.gameObject);
		}
}
