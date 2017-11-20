using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	private ARInteractiveObject interactObject = null;

	void Start () {
		interactObject = gameObject.GetComponent<ARInteractiveObject> ();
		interactObject.OnHit += OnHit;
		interactObject.OnTap += OnTap;
	}

	void OnHit (bool _isHitted) {
	}

	void OnTap () {
	}
}
