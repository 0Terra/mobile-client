using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fountain : MonoBehaviour {

	public ParticleSystem fountainPS = null;
	public TextMeshPro label = null;

	private ARInteractiveObject interactObject = null;
	private bool isHitted = false;

	void Start () {
		OnHit (false);
		interactObject = gameObject.GetComponent<ARInteractiveObject> ();
		interactObject.OnHit += OnHit;
		interactObject.OnTap += OnTap;
	}

	void OnHit (bool _isHitted) {
		isHitted = _isHitted;

		label.color = isHitted ? new Color(1,1,1,1) : new Color(1,1,1,0.4f);
	}

	void OnTap () {
		if (fountainPS.isPlaying) {
			fountainPS.Stop ();
		} else {
			fountainPS.Play ();
		}
	}
}
