using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject DeidaraPrefab;
	public GameObject Predicted;
	public GameObject GameManeger;
	GameObject[] PredictedArray = new GameObject[10];   //指示線の球
    GameObject Deidara;
	Vector2 JumpVec;
    Vector3[] PredictedPos = new Vector3[10];
    bool bSummonsDeidara; //召喚しているかどうか
	bool bJump;

    Vector3 DeidaraPos;
	bool bPairuda;//デイダラに乗っているかどうか
	// Use this for initialization
	void Start () {
		bSummonsDeidara = false;
		bPairuda = false;
		bJump = false;
        ProvisionFPS = 12;
        CountJump = 1;
        PredictedNo = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (bPairuda == false)
        {//パイルダーオンしている時

            //移動
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(0.05f, 0.0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(0.05f, 0.0f);
            }

            if (Input.GetKey(KeyCode.S))
            {//召喚

                if (Deidara == null)
                {
                    Deidara = Instantiate(DeidaraPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f, 0.0f), new Quaternion());
                    DeidaraPos = Deidara.GetComponent<Transform>().position;
                }
                else
                {
                    Deidara.GetComponent<Transform>().position =
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f, DeidaraPos.z);
                }
            }
        }//パイルダーオンしている時
        else
        {//パイルダーオンしていない時

            //移動
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(0.025f, 0.0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(0.025f, 0.0f);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {//パイルダーオフ
                PairudaOff();
            }

            if (Input.GetMouseButtonUp(0))
            {//飛ぶ指示線を出す
                Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousepos = new Vector3(mousepos.x, mousepos.y, transform.position.z);
                Vector2 StartVec, EndVec;
                StartVec = new Vector2((mousepos.x - transform.position.x), 10);
                EndVec = new Vector2(StartVec.x, -10);
                JumpVec = StartVec;

                //エルミート曲線を使う
                if (PredictedArray[0] == null)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        float t = i * 0.1f;
                        PredictedArray[i - 1] = Instantiate(Predicted,
                            GameManeger.gameObject.GetComponent<GameManeger>().HermitePos(
                                transform.position, mousepos, StartVec, EndVec, t)
                    , new Quaternion());
                        PredictedPos[i - 1] = PredictedArray[i - 1].GetComponent<Transform>().position;
                        //Debug.Log(PredictedArray[i - 1].GetComponent<Transform>().position);
                        Debug.Log(PredictedPos[i - 1]);
                    }
                }
                else
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        float t = i * 0.1f;
                        PredictedArray[i - 1].transform.position =
                        GameManeger.gameObject.GetComponent<GameManeger>().HermitePos(
                            transform.position, mousepos, StartVec, EndVec, t);
                        PredictedPos[i - 1] = PredictedArray[i - 1].GetComponent<Transform>().position;
                        Debug.Log(PredictedPos[i - 1]);
                    }
                        
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {//ジャンプ
                //gameObject.GetComponent<Rigidbody> ().AddForce (JumpVec, ForceMode.Impulse);
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                bPairuda = false;
                JumpVec = new Vector2();
                Deidara.GetComponent<Deidara>().Pairuda(bPairuda);
                bJump = true;
                for (int i = 0; i < 10; i++)
                {
                    Destroy(PredictedArray[i]);
                    PredictedArray[i] = null;
                    jumpPlayerPos = gameObject.GetComponent<Transform>().position;
                }
            }
        }//パイルダーオンしていない時
    }
    /// <summary>
    /// デイダラに乗る
    /// </summary>
	public void PairudaOn()
	{
		if (Input.GetKey (KeyCode.P) && bPairuda == false) 
		{
			gameObject.GetComponent<Transform> ().position = 
				new Vector3 (Deidara.GetComponent<Transform> ().position.x,
					1.5f,gameObject.GetComponent<Transform> ().position.z
				);
			gameObject.GetComponent<Rigidbody> ().useGravity = false;
			//gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			//Deidara.GetComponent<Transform> ().parent = transform;
			bPairuda = true;
			Deidara.GetComponent<Deidara> ().Pairuda (bPairuda);
		}
	}
    /// <summary>
    /// デイダラから降りる
    /// </summary>
	void PairudaOff()
	{
		gameObject.GetComponent<Transform> ().position = 
		new Vector3 (gameObject.GetComponent<Transform> ().position.x,
			-1.0f,gameObject.GetComponent<Transform> ().position.z
		);
		gameObject.GetComponent<Rigidbody> ().useGravity = true;
		bPairuda = false;
		Deidara.GetComponent<Deidara> ().Pairuda (bPairuda);
	}

}
