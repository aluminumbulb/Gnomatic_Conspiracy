using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtfulObject : MonoBehaviour {

	public Vector3 destination;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 ray = transform.position;
		RaycastHit2D hit = Physics2D.CircleCast (transform.position, 1.0f, transform.up);
		if (hit) {
			GameObject hitObject = hit.transform.gameObject;
			if (hitObject.GetComponent<Player> ()) {
				//Fixed this, sorry for butting in, tranform isnt implicitly a vector 3, gotta get its position - Adrian
				hitObject.transform.position = destination;
				Debug.Log ("Ouch!");
			}
		}
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (transform.position, destination);
	}
}
