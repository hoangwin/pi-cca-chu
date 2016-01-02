using UnityEngine;
using System.Collections;

public class SaveLoadData : MonoBehaviour {

	public static string STRING_USER_NAME = "USER_NAME";
	public static string STRING_USR_COIN = "USER_COIN";
	public static string STRING_USR_DEVICE_ID = "USR_DEVICE_ID";
	public static string STRING_HASH_COIN = "USR_HASH_COIN";
	public static string STRING_VIP_USER = "STRING_VIP_USER";

	//public static int UserMoney = 0;
	public static string UserName = "User";
	public static string UserDeviceID = "ID";
	public static int UserHashCoin = 0;
	public static int isVipUser = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static void saveGame()
	{
		
		PlayerPrefs.SetInt(STRING_USR_COIN, CoinScore.getOffsetCoin());
		PlayerPrefs.SetString(STRING_USER_NAME, UserName);
		PlayerPrefs.SetString(STRING_USR_DEVICE_ID, UserDeviceID);
		PlayerPrefs.SetInt(STRING_HASH_COIN, UserHashCoin);
		PlayerPrefs.SetInt(STRING_VIP_USER, isVipUser);

		Debug.Log("Save : " + CoinScore.getOffsetCoin());
		PlayerPrefs.Save();
	}
	public static void loadGame()
	{
		CoinScore.setOffsetCoin(PlayerPrefs.GetInt(STRING_USR_COIN));
		Debug.Log("Save : " + CoinScore.getOffsetCoin());
		UserName = PlayerPrefs.GetString(STRING_USER_NAME);
		UserDeviceID = PlayerPrefs.GetString(STRING_USR_DEVICE_ID);
		UserHashCoin = PlayerPrefs.GetInt(STRING_HASH_COIN);
		isVipUser = PlayerPrefs.GetInt(STRING_VIP_USER);

		checkDontCopySaveFile();
		checkDontEditSaveFile();
		if(CoinScore.getRealCoin() < 5)
		{
			CoinScore.setDefaultValue();
		}

	}

	public static void checkDontCopySaveFile()//kiem tra bang DEVICE ID
	{
		if(UserDeviceID == null)
		{
			UserDeviceID = SystemInfo.deviceUniqueIdentifier;
			CoinScore.setDefaultValue();
		}
		//check file save
		else if(!UserDeviceID.Equals(SystemInfo.deviceUniqueIdentifier))
		{
			
			UserDeviceID = SystemInfo.deviceUniqueIdentifier;
			CoinScore.setDefaultValue();
		}
	}

	public static void checkDontEditSaveFile()//Kiem tra bang hash coin
	{

	}
}
