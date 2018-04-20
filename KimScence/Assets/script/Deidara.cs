using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deidara : MonoBehaviour {

	// Use this for initialization
	bool bCatch;
	bool bPairuda;
	GameObject Player;
	Vector3 PlayerPos;
	GameObject Item;
	float MoveSpeed;
	void Start () {
		bCatch = false;
		MoveSpeed = 0.025f;
		Player = GameObject.Find ("Player");
		PlayerPos = Player.GetComponent<Transform> ().position;
		bPairuda = false;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerPos = Player.GetComponent<Transform> ().position;
		if (PlayerPos.x > GetComponent<Transform> ().transform.position.x) {
			GetComponent<Transform> ().transform.position += new Vector3(MoveSpeed,0.0f);
		}
		if (PlayerPos.x < GetComponent<Transform> ().transform.position.x) {
			GetComponent<Transform> ().transform.position -= new Vector3(MoveSpeed,0.0f);
		}
		if (bCatch == true && Input.GetKey(KeyCode.R)) {
			relese ();
		}
	}
	//void OnCollisionEnter(Collision col)
	//{
	//	Debug.Log (col.transform);
	//	if (Input.GetKeyDown (KeyCode.C) && bCatch == false) {
	//		bCatch = true;
	//
	//	}
	//}
	void OnTriggerStay(Collider col)
	{
		if (col.tag == "item") {
			if (Input.GetKeyDown (KeyCode.C) && bCatch == false && bPairuda == false) {
				bCatch = true;
				Item = col.gameObject;
				col.gameObject.GetComponent<Transform> ().parent = transform;
				col.gameObject.GetComponent<Transform> ().localPosition = new Vector3 (0.0f, 0.5f);
			}
		}
	}
	void relese()
	{
		Item.gameObject.GetComponent<Transform>().position =
			new Vector3(Item.gameObject.GetComponent<Transform>().position.x,
				0.0f);
		Item.gameObject.GetComponent<Transform> ().parent = null;
		bCatch = false;
	}
	public void Pairuda(bool pairuflg)
	{
		bPairuda = pairuflg;
	}
}
