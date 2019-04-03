using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomChange : MonoBehaviour {

	public string levelName;

	void Update(){
		RaycastHit2D surrounding = Physics2D.CircleCast(transform.position,0.3f,new Vector2(0,0));
		if (surrounding.collider != null) {
			Debug.Log ("I'm touching Something");
			SceneManager.LoadScene (levelName, LoadSceneMode.Single);
		}
	}
}	
