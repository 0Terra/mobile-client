using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.iOS;

public class AREnvironmentManager : MonoBehaviour {

	public Shadow shadow;
	public GameObject buttonMap;
	public GameObject buttonObjects;
	public GameObject logo;
	public GameObject findPlane;
	public ARCameraManager arCameraManager;
	public GameObject sceneRootPrefab;
	public GameObject objectPrefab;
	private ARPlaneAnchorManager arPlaneAnchorManager;
	private List<GameObject> objects;

	IEnumerator Start() {
		arPlaneAnchorManager = new ARPlaneAnchorManager ();

		// showing of the scene
		shadow.gameObject.SetActive (true);
		logo.SetActive (true);
		yield return new WaitForSecondsRealtime (2.0f);
		logo.SetActive (false);
		yield return StartCoroutine (shadow.FadeOut());

		//find ground surface
		findPlane.SetActive(true);
		yield return new WaitForSecondsRealtime (2.0f);

		while (arPlaneAnchorManager.GetCurrentPlaneAnchors ().Count == 0) {
			yield return new WaitForSecondsRealtime (1.0f);
		}

		findPlane.SetActive(false);
		arCameraManager.RunSession (false, false);

		Vector3 pos = UserData.Instance.CenterOffset;
		pos -= UnityARMatrixOps.GetPosition (arCameraManager.Session.GetCameraPose ());
		pos.y = UnityARMatrixOps.GetPosition (arPlaneAnchorManager.GetCurrentPlaneAnchors () [0].transform).y;
		objects.Add (Instantiate (sceneRootPrefab, pos, Quaternion.identity));

//		Vector3 pos01 = Vector3.zero;
//		pos01.y = UnityARMatrixOps.GetPosition (arPlaneAnchorManager.GetCurrentPlaneAnchors () [0].transform).y;
//		objects.Add (Instantiate (sceneRootPrefab, pos01, Quaternion.identity));
	}

	public void OnBackToMap_Click () {
		buttonMap.SetActive (false);
		buttonObjects.SetActive (false);
		findPlane.SetActive(false);
		StartCoroutine (GotoMap());
	}

	public void OnAddObjects_Click () {

		var screenPosition = Camera.main.ScreenToViewportPoint(new Vector2(Screen.width / 2.0f, Screen.height / 2.0f));
		ARPoint point = new ARPoint {
			x = screenPosition.x,
			y = screenPosition.y
		};

		List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, ARHitTestResultType.ARHitTestResultTypeExistingPlane);

		if (hitResults.Count > 0) {
			objects.Add (Instantiate(objectPrefab, UnityARMatrixOps.GetPosition (hitResults[0].worldTransform),
				UnityARMatrixOps.GetRotation (hitResults[0].worldTransform)));
		}
	}

	private IEnumerator GotoMap() {
		yield return StartCoroutine (shadow.FadeIn());
		SceneManager.LoadScene ("Map");
	}

	void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus) {
			SceneManager.LoadScene ("AREnvironment");
		}
	}
}
