using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManeger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// エルミート曲線の計算
    /// </summary>
    /// <param name="StartPos">スタートポス</param>
    /// <param name="EndPos">エンドポス</param>
    /// <param name="StartVec">スタートのベクトル</param>
    /// <param name="EndVec">エンドのベクトル</param>
    /// <param name="time">割る時間</param>
    /// <returns>ポジション</returns>
	public Vector2 HermitePos(Vector2 StartPos,Vector2 EndPos,Vector2 StartVec,Vector2 EndVec,float time)
	{
		Vector2 Pos;
		float h00, h01, h10, h11;
		h00 = (2 * time * time * time) - (3 * time * time) + 1;
		h01 = (-2 * time * time * time) + (3 * time * time);
		h10 = (time * time * time) - (2 * time * time) + time;
		h11 = (time * time * time) - (time * time);

		Pos.x = (
			h00 * StartPos.x   + h01 * EndPos.x +
			h10 * StartVec.x +	h11 * EndVec.x);
		Pos.y = (
			h00 * StartPos.y + h01 * EndPos.y +
			h10 *StartVec.y + h11 * EndVec.y);

		return Pos;
	}
}
