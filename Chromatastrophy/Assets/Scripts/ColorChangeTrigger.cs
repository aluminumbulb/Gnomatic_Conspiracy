using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour {
	private CircleCollider2D orb;
	public GameController gameController;
	public bool Red_Shift = false, Green_Shift = false, Blue_Shift = false;
	private List<Collider2D> colliders;
	private HurtfulObject[] spikes;

	// Use this for initialization
	void Start () { 
		gameController = GameObject.FindObjectOfType<GameController> ();
		spikes = GameObject.FindObjectsOfType<HurtfulObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		colliders = new List<Collider2D> ();
		RaycastHit2D surrounding = Physics2D.CircleCast(transform.position,1.0f,new Vector2(0,0));
		if (surrounding.collider != null && surrounding.collider.tag == "Player") {
			gotten ();
		}
	}

	void gotten(){
		gameController.lastSavedLocation = transform.position;
		foreach (HurtfulObject spike in spikes) {
			spike.destination = transform.position;
		}

		gameController.orbGetRed = Red_Shift;
		gameController.orbGetGreen = Green_Shift;
		gameController.orbGetBlue = Blue_Shift;

		gameController.paintWorld ();
		Destroy (this.gameObject);
		}
}
