using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;

public class UserData {
	private static UserData instance;

	public static UserData Instance {
		get {
			if (instance == null) {
				instance = new UserData ();
			}
			return instance;
		}
	}

	public Vector3 CenterOffset;
	public Coordinates CurrentRegioID;
}
