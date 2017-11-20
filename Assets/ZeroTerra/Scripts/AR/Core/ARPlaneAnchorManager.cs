using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using System.Linq;

public class ARPlaneAnchorManager {

	private Dictionary<string, ARPlaneAnchor> planeAnchorMap;


	public ARPlaneAnchorManager ()
	{
		planeAnchorMap = new Dictionary<string,ARPlaneAnchor> ();
		UnityARSessionNativeInterface.ARAnchorAddedEvent += AddAnchor;
		UnityARSessionNativeInterface.ARAnchorUpdatedEvent += UpdateAnchor;
		UnityARSessionNativeInterface.ARAnchorRemovedEvent += RemoveAnchor;

	}


	public void AddAnchor(ARPlaneAnchor arPlaneAnchor)
	{
		planeAnchorMap.Add (arPlaneAnchor.identifier, arPlaneAnchor);
	}

	public void RemoveAnchor(ARPlaneAnchor arPlaneAnchor)
	{
		if (planeAnchorMap.ContainsKey (arPlaneAnchor.identifier)) {
			planeAnchorMap.Remove (arPlaneAnchor.identifier);
		}
	}

	public void UpdateAnchor(ARPlaneAnchor arPlaneAnchor)
	{
		if (planeAnchorMap.ContainsKey (arPlaneAnchor.identifier)) {
			planeAnchorMap [arPlaneAnchor.identifier] = arPlaneAnchor;
		}
	}

	public void Destroy()
	{
		planeAnchorMap.Clear ();
	}

	public List<ARPlaneAnchor> GetCurrentPlaneAnchors()
	{
		return planeAnchorMap.Values.ToList ();
	}
}
