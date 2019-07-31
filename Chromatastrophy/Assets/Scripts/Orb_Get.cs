using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_Get : MonoBehaviour {
	private CircleCollider2D orb;
	public GameController gameController;
	public GameObject spikeToAppear;

	public enum orbType{Red, Green, Blue};
	public orbType myOrbType;


	// Use this for initialization
	void Start () { 
		gameController = GameObject.FindObjectOfType<GameController> ();
	}
		
	// Update is called once per frame
	void LateUpdate () {
		RaycastHit2D surrounding = Physics2D.CircleCast(transform.position,0.65f,new Vector2(0,0));
		if (surrounding.collider != null) {
			gotten ();
		}
	}

	public void gotten(){
			if(myOrbType == orbType.Red){
				gameController.orbGetRed = true;
				spikeToAppear.SetActive (true);	
				gameController.audioSource.Stop ();
				
			}

			if (myOrbType == orbType.Green) {
				gameController.orbGetGreen = true;
				spikeToAppear.SetActive (true);
				gameController.audioSource.Stop ();
			}

			if (myOrbType == orbType.Blue) {
				gameController.orbGetBlue = true;
				spikeToAppear.SetActive (true);
				gameController.audioSource.Stop ();
			}

			//spikeToAppear.SetActive (true);
			
			gameController.paintWorld ();
			Destroy (this.gameObject);
		}
}
