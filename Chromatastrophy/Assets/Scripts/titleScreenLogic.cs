using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreenLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void loadScene(){
		SceneManager.LoadScene ("Hub Scene");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
