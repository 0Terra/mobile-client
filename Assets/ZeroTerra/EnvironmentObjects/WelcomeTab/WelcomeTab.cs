using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WelcomeTab : MonoBehaviour {

	public TextMeshPro welcomeText = null;
	public TextMeshPro label = null;
	public Transform boardTransform;

	private ARInteractiveObject interactObject = null;
	private bool isHitted = false;
	private bool isHitLocked = false;

	IEnumerator Start () {
		OnHit (false);
		interactObject = gameObject.GetComponent<ARInteractiveObject> ();
		interactObject.OnHit += OnHit;
		interactObject.OnTap += OnTap;

		while (true) {
			boardTransform.Rotate (Vector3.up);
			yield return 0;
		}
	}

	void OnHit (bool _isHitted) {
		if (isHitLocked)
			return;

		isHitted = _isHitted;

		label.color = isHitted ? new Color(1,1,1,1) : new Color(1,1,1,0.4f);
	}

	void OnTap () {
		if (isHitLocked)
			return;
		
		OnHit (false);
		isHitLocked = true;
		StartCoroutine (IE_CheckKeyboardStatus());
	}
		
	IEnumerator IE_CheckKeyboardStatus() {
		TouchScreenKeyboard keyboard = TouchScreenKeyboard.Open ("");

		while (keyboard.status == TouchScreenKeyboard.Status.Visible) {
			yield return new WaitForSecondsRealtime (0.1f);
		}

		if (!keyboard.wasCanceled) {
			welcomeText.text = keyboard.text;
		}

		isHitLocked = false;
	}
}
