using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject player;
	private Vector3 camPos;

	void Start () {
		camPos = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
	}

	void Update () {
		camPos = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
		transform.position = camPos;
	}
}
