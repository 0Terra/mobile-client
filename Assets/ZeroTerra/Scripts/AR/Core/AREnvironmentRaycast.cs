using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AREnvironmentRaycast : MonoBehaviour {

	public Camera targetCamera;
	ARInteractiveObject lastHitted = null;
	ARInteractiveObject currHitted = null;

	void Start() {
		StartCoroutine (IE_Hit());
	}

	IEnumerator IE_Hit() {

		while (true) {
			RaycastHit hitInfo;

			if (Physics.Raycast (targetCamera.transform.position, targetCamera.transform.forward, out hitInfo, 1.5f)) {

				currHitted = hitInfo.collider.gameObject.GetComponent<ARInteractiveObject> ();

				if (currHitted != lastHitted) {

					if (lastHitted != null) {
						lastHitted.Hit (false);
					}

					if (currHitted != null) {
						currHitted.Hit (true);
					}
				}

				lastHitted = currHitted;
			} else {
				if (lastHitted != null) {
					lastHitted.Hit (false);
				}

				currHitted = null;
				lastHitted = null;
			}

			yield return new WaitForSeconds (0.1f);
		}
	}

	void Update () {

		if (Input.touchCount > 0 && currHitted != null) {
			
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
				currHitted.Tap ();
			}
		}
	}

}
