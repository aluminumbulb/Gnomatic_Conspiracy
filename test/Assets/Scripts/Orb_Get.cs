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
		//orb = GetComponent<CircleCollider2D> ();
		//myOrbType = orbType.Red;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D surrounding = Physics2D.CircleCast(transform.position,0.65f,new Vector2(0,0));
		if (surrounding.collider != null) {
			gotten ();
		}
	}

	void gotten(){
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
