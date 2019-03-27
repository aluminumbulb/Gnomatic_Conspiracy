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
		
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (transform.position, destination);
	}
}
