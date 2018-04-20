using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject DeidaraPrefab;
	GameObject Deidara;
	bool bSummonsDeidara;
	Vector3 DeidaraPos;
	bool bPairuda;
	// Use this for initialization
	void Start () {
		bSummonsDeidara = false;
		bPairuda = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (bPairuda == false) {
			if (Input.GetKey (KeyCode.A)) {
				transform.position -= new Vector3 (0.05f, 0.0f);
			}
			if (Input.GetKey (KeyCode.D)) {
				transform.position += new Vector3 (0.05f, 0.0f);
			}
			if (Input.GetKey (KeyCode.S) && bSummonsDeidara == false) {
				bSummonsDeidara = true;
				Deidara = Instantiate (DeidaraPrefab, new Vector3 (gameObject.transform.position.x, 0.0f), new Quaternion ());
				DeidaraPos = Deidara.GetComponent<Transform> ().position;
				Debug.Log (DeidaraPos);
			} else if (Input.GetKey (KeyCode.S)) {
				Deidara.GetComponent<Transform> ().position = 
				new Vector3 (GetComponent<Transform> ().position.x, DeidaraPos.y, DeidaraPos.z);
			}
		} else {
			if (Input.GetKey (KeyCode.A)) {
				transform.position -= new Vector3 (0.025f, 0.0f);
			}
			if (Input.GetKey (KeyCode.D)) {
				transform.position += new Vector3 (0.025f, 0.0f);
			}
			if (Input.GetKeyDown (KeyCode.O)) {
				PairudaOff ();
			}
		}

	}
	public void PairudaOn()
	{
		if (Input.GetKey (KeyCode.P) && bPairuda == false) 
		{
			
			gameObject.GetComponent<Transform> ().position = 
				new Vector3 (Deidara.GetComponent<Transform> ().position.x,
					1.5f,gameObject.GetComponent<Transform> ().position.z
				);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			//Deidara.GetComponent<Transform> ().parent = transform;
			bPairuda = true;
			Deidara.GetComponent<Deidara> ().Pairuda (bPairuda);
		}
	}
	void PairudaOff()
	{
		gameObject.GetComponent<Transform> ().position = 
		new Vector3 (gameObject.GetComponent<Transform> ().position.x,
			-1.0f,gameObject.GetComponent<Transform> ().position.z
		);
		bPairuda = false;
		Deidara.GetComponent<Deidara> ().Pairuda (bPairuda);
	}
}
