using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomChange : MonoBehaviour {

	public string levelName;

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Entered");
		if(other.tag == "Player"){
			SceneManager.LoadScene (levelName, LoadSceneMode.Single);
		}
	}
}
