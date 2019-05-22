using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors_TutorialText : MonoBehaviour {
	private bool t_needTutorial = true;
	public GameObject tutorialText;
	private float timer = 60;
	// Use this for initialization
	void Start () {
			gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		//if(!GameController.orbGetRed){

		if (t_needTutorial) {
			tutorialText.SetActive (true);
		} else {
			timer--;
			if(timer == 0){
		    	tutorialText.SetActive (false);
			}
		}

		if(Input.GetKey("up")||Input.GetKey("down")||Input.GetKey("right")||Input.GetKey("left")){
			t_needTutorial = false;
		}
		if(Input.GetKey("w")||Input.GetKey("s")||Input.GetKey("d")||Input.GetKey("a")){	
			t_needTutorial = false;
		}	

	}
}