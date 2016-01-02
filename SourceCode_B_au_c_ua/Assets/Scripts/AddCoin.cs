using UnityEngine;
using System.Collections;

public class AddCoin : MonoBehaviour {

	public GameObject panelConFirmAddCoin =null;
	public GameObject labelTextConFirm5000 = null;
	public GameObject labelTextConFirm10000 = null;
	public GameObject labelTextConFirm15000 = null;
	public static AddCoin instance = null;
	public static WWW www = null;
	public static int indexSMS = 0;//= = 5000, 1 =10000, 2 =15000
	public static bool isConfirm = false;
	public static bool isWaitingServer = false;
	void Start () {
		DEF.Init();
		isConfirm= false;
		GameObject hand = GameObject.Find("BackGround");
		//GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity,new  Vector3(DEF.scaleX, DEF.scaleY, 1));
		
		if(hand!= null)
		{
			//	DEF.ResizeBySize(hand,DEF.DISPLAY_WIDTH,DEF.DISPLAY_HEIGHT);
			hand.transform.localScale = new Vector3 (DEF.sx_ortho, DEF.sy_ortho, 1);
		}
		panelConFirmAddCoin = GameObject.Find("PanelConFirmAddCoin");
		labelTextConFirm5000 = GameObject.Find("LabelTextConFirm5000"); 
		labelTextConFirm10000 = GameObject.Find("LabelTextConFirm10000"); 
		labelTextConFirm15000 = GameObject.Find("LabelTextConFirm15000");
		GameObject.Find("LabelCurrentCoin").GetComponent<UILabel>().text = "Coin : " +CoinScore.getRealCoin();

		NGUITools.SetActive(panelConFirmAddCoin,false);
		instance = this;
	//test
		//string mobile = "0986742650";
		//Application.OpenURL("sms:" + mobile);
		//Application.OpenURL("sms:" + mobile + "?body=" + msg);
	//test
		DEF.ScaleAnchorGui();
	}


	void OnApplicationPause (bool pause)
	{
		if(pause)
		{
			Debug.Log("IsPause");
			// we are in background
		}
		else
		{
			CheckAddCoininServer();
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			EscapePress();

		}

		if(isWaitingServer)
		{
			if(www.isDone)
			{		
				isWaitingServer = false;
				Debug.Log("WW return :" +www.text);
				if(www.text.Length>0)
				{
					int i =int.Parse(www.text);
					if(i>0)
					{
						CoinScore.addScore(i);
						SaveLoadData.isVipUser = 1;
						SaveLoadData.saveGame();
						GameObject.Find("LabelSupportNetwork").GetComponent<UILabel>().text = "Bạn nhận " +i +" Coin" ;
						GameObject.Find("LabelCurrentCoin").GetComponent<UILabel>().text = "Coin : " +CoinScore.getRealCoin();
						WWW www1 = new WWW("http://gamethuanviet.com/baucuatomca/DeleteCoin.php?username="+ SaveLoadData.UserName);
#if UNITY_ANDROID
						ButtonAddCoin.stopADS();
#elif UNITY_WP8
						WP8Statics.StopAds("");
#endif
					}
					else
					{
						GameObject.Find("LabelSupportNetwork").GetComponent<UILabel>().text = "Bạn nhận được 0 Coin" ;
					}
				}
			}

		}
	}
	public static void EscapePress()
	{
		if(isConfirm)
		{
			isConfirm =false;
			NGUITools.SetActive(AddCoin.instance.panelConFirmAddCoin,false);
		}
		else
		{
			Debug.Log("Back from Help");
			Application.LoadLevel("GamePlay");
		}
	}

	public void CheckAddCoininServer()
	{
		isWaitingServer = true;
		www = null;
		www = new WWW("http://gamethuanviet.com/baucuatomca/GetCoin.php?username="+ SaveLoadData.UserName);
		Debug.Log("WWW : " +"http://gamethuanviet.com/baucuatomca/GetCoin.php?username="+ SaveLoadData.UserName);
		GameObject.Find("LabelSupportNetwork").GetComponent<UILabel>().text = "server...\n Đang kết nối đến ";
		//	if(www.text.Trim().Equals("FAIL"))
		
	}
}
