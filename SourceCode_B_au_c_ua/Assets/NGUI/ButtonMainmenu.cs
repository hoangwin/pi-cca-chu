using UnityEngine;
using System.Collections;

public class ButtonMainmenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void adasdasd()
	{
		Debug.Log("tona");
	}
	UILabel labelinput;
	void OkInputPress()
	{

		UILabel[] allChildren = this.gameObject.GetComponentsInChildren<UILabel>();
		foreach (UILabel child in allChildren) 
		{
			if(child.name =="LabelInputName")
				labelinput = child;
		}
		string str = labelinput.text;
		str = str.Trim();
		str = str.Replace("'","_");
		str = str.Replace("\"","_");
		str = str.Replace(" ","_");
		str =str.Replace("=","_");
		labelinput.text = str;
		//Debug.Log("Press OK at Input Name Screen: Name:" + str);

		if(str.Length <6)
		{
			GameObject.Find("LabelInputNotification").GetComponent<UILabel>().text = "Tên quá ngắn. Nhập lại.";
		}
		else
		{
			SaveLoadData.UserName = str; 
			
			GameObject.Find("LabelInputNotification").GetComponent<UILabel>().text = "Connect...";
			int i = checkUserInput();
			if(i==0)
			{
				if(MainMenu.panelInput != null)
					NGUITools.SetActive(MainMenu.panelInput,false);     
				if(MainMenu.panelMenu != null)
				{
					//	Debug.Log ("OK Sao Ta");
					NGUITools.SetActive(MainMenu.panelMenu,true);     
				}
				MainMenu.mSubState = MainMenu.SUB_STATE_MAINMENU;
			}
			else if(i ==1)
			{
				GameObject.Find("LabelInputNotification").GetComponent<UILabel>().text = "User này đã có người sử dụng";
			}else if(i == 2)
			{
				GameObject.Find("LabelInputNotification").GetComponent<UILabel>().text = "Không kết nối được với Server";
				SaveLoadData.UserName = "USER";
			}
			SaveLoadData.saveGame();
		}
	}
	int checkUserInput()//0 = ok;1 = trung;2 = ko ket noi dc server
	{
	
		WWW www = new WWW("http://gamethuanviet.com/baucuatomca/SignUp.php?username="+ SaveLoadData.UserName);
		while(!www.isDone)
		{
		}
		Debug.Log("WW return :" +www.text);
		if(www.text.Trim().Equals("FAIL"))
			return 1;
		if(www.text.Trim().Equals("SUCCESS"))
			return 0;
		return 2;

		
	}

	void CancelInputPress()
	{
		SaveLoadData.UserName = "USER"; 
    	NGUITools.SetActive(MainMenu.panelInput,false);     
		if(MainMenu.panelMenu != null)
		{
			//	Debug.Log ("OK Sao Ta");
			NGUITools.SetActive(MainMenu.panelMenu,true);     
		}
		MainMenu.mSubState = MainMenu.SUB_STATE_MAINMENU;
		SaveLoadData.saveGame();
	}


	void PlayButtonPress()
	{
		SoundEngine.playSounBack(this);
		Application.LoadLevel("GamePlay");
		
	}
	void RankingButtonPress()
	{
		SoundEngine.playSounBack(this);
		Ranking.isMoveFromMainmenu = true;
		Application.LoadLevel("Ranking");
		
	}
	void AddCoinButtonPress()
	{
		SoundEngine.playSounBack(this);
		Application.LoadLevel("Help");
		
	}
	void SoundButtonPress()
	{
		SoundEngine.OptionSoundClick();
		//Application.LoadLevel("Help");
		UILabel label =  GameObject.Find("LabelSoundOnOff").GetComponent<UILabel>();
		if(SoundEngine.isSoundEnable)
		{

			label.text = "Âm Thanh : Bật";
		}else
		{
			label.text = "Âm Thanh : Tắt";
		}
		
	}
	void ExitButtonPress()
	{
	//	ButtonMainmenu.Mai
		SoundEngine.playSounBack(this);
		MainMenu.EscapePress();
		Debug.Log("Exit Press");
		
	}

	void YesConfirmPress()
	{
		SoundEngine.playSounBack(this);
		Application.Quit();

	}
	void NoConfirmPress()
	{
		SoundEngine.playSounBack(this);
		MainMenu.isConfirmShow = false;
		NGUITools.SetActive(MainMenu.panelConfirm,false);     
		if(MainMenu.mSubState == MainMenu.SUB_STATE_INPUT)
		{
			NGUITools.SetActive(MainMenu.panelInput,true);    
			
		}
		else
			NGUITools.SetActive(MainMenu.panelMenu,true);    

	}

	void updateInputText()
	{
		//myUIInput.text = myUIInput.text.ToLower();
	}
}
