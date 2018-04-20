using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Deidara") {
			gameObject.GetComponentInParent<Player> ().PairudaOn ();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
