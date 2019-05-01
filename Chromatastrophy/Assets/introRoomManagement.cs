using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introRoomManagement : MonoBehaviour {
	public Animator _camera;
	public Animator _title;
	// Use this for initialization
	void Start () {
		_camera.SetBool ("starting", true);
		_title.SetBool ("starting", true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
