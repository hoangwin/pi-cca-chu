using UnityEngine;
using System.Collections;

public class ButtonAddCoin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void EscapePress()
	{
		Debug.Log("Back from Adcoin");
		Application.LoadLevel("GamePlay");
	}

	public void ButtonAdd5000Press()
	{
		AddCoin.indexSMS = 0;
		AddCoin.isConfirm = true;
		NGUITools.SetActive(AddCoin.instance.panelConFirmAddCoin,true);
		NGUITools.SetActive(AddCoin.instance.labelTextConFirm10000,false);
		NGUITools.SetActive(AddCoin.instance.labelTextConFirm5000,true);
		NGUITools.SetActive(AddCoin.instance.labelTextConFirm15000,false);
		GameObject.Find("LabelTextConFirmSMSMessage").GetComponent<UILabel>().text = "Gởi : 7595\nNMH BCTC " + SaveLoadData.UserName+ "\nSoạn tin:";
		
	}
	public void ButtonAdd10000Press()
	{
		AddCoin.indexSMS = 1;
		AddCoin.isConfirm = true;
		NGUITools.SetActive(AddCoin.instance.panelConFirmAddCoin,true);
		NGUITools.SetActive(AddCoin.instance.labelTextConFirm10000,true);
		NGUITools.SetActive(AddCoin.instance.labelTextConFirm5000,false);
		NGUITools.SetActive(AddCoin.instance.labelTextConFirm15000,false);		
		GameObject.Find("LabelTextConFirmSMSMessage").GetComponent<UILabel>().text = "Gởi : 7695\nNMH BCTC " + SaveLoadData.UserName+ "\nSoạn tin:";
	}
	public void ButtonAdd15000Press()
	{
		AddCoin.indexSMS = 2;
		AddCoin.isConfirm = true;
		NGUITools.SetActive(AddCoin.instance.panelConFirmAddCoin,true);
		NGUITools.SetActive(AddCoin.instance.labelTextConFirm10000,false);
		NGUITools.SetActive(AddCoin.instance.labelTextConFirm5000,false);
		NGUITools.SetActive(AddCoin.instance.labelTextConFirm15000,true);		
		GameObject.Find("LabelTextConFirmSMSMessage").GetComponent<UILabel>().text = "Gởi : 7795\nNMH BCTC " + SaveLoadData.UserName+ "\nSoạn tin:";
	}
	public void ButtonConfirmCancelPress()
	{
		NGUITools.SetActive(AddCoin.instance.panelConFirmAddCoin,false);
		AddCoin.isConfirm = false;
	}

	public void CheckAddCoinPress()
	{
		AddCoin.instance.CheckAddCoininServer();
	
	}
	public void ButtonConfirmOKPress()
	{
#if UNITY_ANDROID || UNITY_WP8
		sendSMS();
#endif
	}

	public void sendSMS()
	{

		Debug.Log ("SEND SMS");
		string mobile = "7595";			
		string message = "NMH BCTC " + SaveLoadData.UserName;		
		switch(AddCoin.indexSMS)
		{
		case 0:
			mobile = "7595";	
			//Application.OpenURL("sms:" + mobile + "?body=NMH BCTC " + SaveLoadData.UserName);
			//Debug.Log("sms:" + mobile + "?body=NMH BCTC " + SaveLoadData.UserName);
			break;
		case 1:
			mobile = "7695";
			//Application.OpenURL("sms:" + mobile + "?body=NMH BCTC "+ SaveLoadData.UserName);
			//Debug.Log("sms:" + mobile + "?body=NMH BCTC " + SaveLoadData.UserName);
			break;
		case 2:
			mobile = "7795";
			//Application.OpenURL("sms:" + mobile + "?body=NMH BCTC "+ SaveLoadData.UserName);
			//Debug.Log("sms:" + mobile + "?body=NMH BCTC " + SaveLoadData.UserName);
			break;
		}
		ButtonConfirmCancelPress();
		SendSMS(mobile,message);
	}

	public  static void SendSMS(string number, string Message)
	{
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.hcg.baucua.tomcacop.UnityPlayerNativeActivity"))
        {
			jc.CallStatic<int>("OpenSMS",number,Message);
		}
#elif UNITY_WP8
		WP8Statics.SendSMS(number+"|"+ Message);
#endif
	}
	public  static void stopADS()
	{
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.hcg.baucua.tomcacop.UnityPlayerNativeActivity"))
        {
			jc.CallStatic<int>("StopShowAds");
		}
#elif UNITY_WP8
		WP8Statics.StopAds("");
#endif
	}


	//public  void getSMSCodeResult(string  Message)
	//{
	//	if(Message.Trim().Equals("")		   ;
	//}


}
