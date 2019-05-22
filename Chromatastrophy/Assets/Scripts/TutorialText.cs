using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialText : MonoBehaviour {

	private bool t_needTutorial = true;
	public GameObject tutorialText;
	// Use this for initialization
	void Start () {
		gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
				if (!t_needTutorial) {
			tutorialText.SetActive (false);
		}
		
			if(Input.GetKey("up")||Input.GetKey("down")||Input.GetKey("right")||Input.GetKey("left")){
			t_needTutorial = false;
		}
			if(Input.GetKey("w")||Input.GetKey("s")||Input.GetKey("d")||Input.GetKey("a")){	
			t_needTutorial = false;
		}	
					
	}
}
