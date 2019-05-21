using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {
	public GameObject loadingScreen;
	public Slider slider;

	public void LoadLevel(string sceneName){
		StartCoroutine (LoadAsynchronously (sceneName));
	}


	IEnumerator LoadAsynchronously(string sceneName){
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);

		loadingScreen.SetActive (true);


		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			slider.value = progress;
			yield return null;
		}
	}
}
