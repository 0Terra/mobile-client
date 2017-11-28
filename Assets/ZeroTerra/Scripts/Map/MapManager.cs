using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

	public Shadow shadow;
	public GameObject panel;
	public GameObject logo;
	public GOMapExtension goMap;

	private bool isInited = false;

	IEnumerator Start() {
		shadow.gameObject.SetActive (true);
		logo.SetActive (true);
		yield return new WaitForSecondsRealtime (2.0f);
		logo.SetActive (false);
		StartCoroutine (shadow.FadeOut());
	}

	public void OnMapLoaded() {
		isInited = true;
	}

	public void OnARMode_Click () {
		panel.SetActive (false);

		Debug.Log (UserData.Instance.LandInfo.ToString());

		StartCoroutine (GotoAREnvironment());
	}

	private IEnumerator GotoAREnvironment() {
		yield return StartCoroutine (shadow.FadeIn());
		SceneManager.LoadScene ("AREnvironment");
	}
}
