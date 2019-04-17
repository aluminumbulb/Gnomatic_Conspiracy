using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class footsteps : MonoBehaviour {

	[SerializeField]
	private AudioClip[] clips;

	private AudioSource audioSource;

	// Use this for initialization
	private void Awake () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void step(){
		AudioClip clip = GetRandomClip ();
		audioSource.PlayOneShot(clip);

	}

	private AudioClip GetRandomClip(){
		return clips[UnityEngine.Random.Range(0, clips.Length)];
	}

}
