using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shadow : MonoBehaviour {

	private Image shadowImage = null;

	void Init() {
		if (shadowImage == null) {
			shadowImage = GetComponent<Image> ();
		}
	}

	public IEnumerator FadeIn() {
		Init ();
		Color currColor = shadowImage.color;
		shadowImage.gameObject.SetActive (true);

		while (currColor.a < 1.0f) {
			currColor.a = Mathf.Min (currColor.a + 2.0f * Time.unscaledDeltaTime, 1.0f);
			shadowImage.color = currColor;
			yield return 0;
		}
	}

	public IEnumerator FadeOut() {
		Init ();
		Color currColor = shadowImage.color;

		while (currColor.a > 0.0f) {
			currColor.a = Mathf.Max (currColor.a - 2.0f * Time.unscaledDeltaTime, 0.0f);
			shadowImage.color = currColor;
			yield return 0;
		}

		shadowImage.gameObject.SetActive (false);
	}
}
