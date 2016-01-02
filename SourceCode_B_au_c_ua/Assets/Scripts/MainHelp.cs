using UnityEngine;
using System.Collections;

public class MainHelp : MonoBehaviour {


	void Start () {
		DEF.Init();
		GameObject hand = GameObject.Find("BackGround");
		//GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity,new  Vector3(DEF.scaleX, DEF.scaleY, 1));
		
		if(hand!= null)
		{
			//	DEF.ResizeBySize(hand,DEF.DISPLAY_WIDTH,DEF.DISPLAY_HEIGHT);
			hand.transform.localScale = new Vector3 (DEF.sx_ortho, DEF.sy_ortho, 1);
		}
		//sendSMSBrige.SendSMS();
	//test
		//string mobile = "0986742650";
		//Application.OpenURL("sms:" + mobile);
		//Application.OpenURL("sms:" + mobile + "?body=" + msg);
	//test
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			EscapePress();

		}
	}
	public static void EscapePress()
	{
		Debug.Log("Back from Help");
		Application.LoadLevel("MainMenu");
	}
}
