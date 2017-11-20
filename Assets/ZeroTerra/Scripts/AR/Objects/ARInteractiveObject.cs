using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARInteractiveObject : MonoBehaviour {

	public event Action<bool> OnHit = delegate {};
	public event Action OnTap = delegate {};

	public virtual void Hit(bool isHitted) {
		OnHit (isHitted);
	}

	public virtual void Tap() {
		OnTap ();
	}
}
