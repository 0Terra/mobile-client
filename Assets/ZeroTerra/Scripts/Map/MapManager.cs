using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

	public Shadow shadow;
	public GameObject buttonAR;
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
		buttonAR.SetActive (false);
		UserData.Instance.CurrentRegioID = goMap.GetCoordinateCurrentRegionID ();
		UserData.Instance.CenterOffset = goMap.GetOffsetFromCenterCurrentRegion ();

		Debug.Log ("CurrentRegionID: " + UserData.Instance.CurrentRegioID);
		Debug.Log ("CenterOffset: " + UserData.Instance.CenterOffset);

		StartCoroutine (GotoAREnvironment());
	}

	private IEnumerator GotoAREnvironment() {
		yield return StartCoroutine (shadow.FadeIn());
		SceneManager.LoadScene ("AREnvironment");
	}
}
