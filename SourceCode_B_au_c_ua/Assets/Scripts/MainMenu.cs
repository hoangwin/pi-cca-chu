using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	public static GameObject panelInput = null;
	public static GameObject panelMenu = null;
	public static GameObject panelConfirm = null;
	public static int mSubState = 0;
	public static bool isConfirmShow = false;
	public static int SUB_STATE_INPUT = 0;
	public static int SUB_STATE_MAINMENU = 1;

	void Start () {
		Debug.Log("olala :Start");
		isConfirmShow = false;

		DEF.Init();

		GameObject hand = GameObject.Find("BackGround");
		//GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity,new  Vector3(DEF.scaleX, DEF.scaleY, 1));
			panelInput = GameObject.Find("PanelInput");
		panelMenu = GameObject.Find("PanelMenu");
		panelConfirm = GameObject.Find("PanelConFirm");
		SaveLoadData.loadGame();
		ShowPanelAtBegin();
		SoundEngine.initSound();
		if(hand!= null)
		{
		//	DEF.ResizeBySize(hand,DEF.DISPLAY_WIDTH,DEF.DISPLAY_HEIGHT);
			hand.transform.localScale = new Vector3 (DEF.sx_ortho, DEF.sy_ortho, 1);
		}
		DEF.ScaleAnchorGui();
		//checkMyApp();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			EscapePress();
		}
	}
	public void ShowPanelAtBegin()
	{
		if(SaveLoadData.UserName.Length >= 6)
		{

			Debug.Log("Tenten");
			mSubState = SUB_STATE_MAINMENU;
			if(panelInput!=null)
				NGUITools.SetActive(panelInput,false);   
			if(panelConfirm != null)
			{
				NGUITools.SetActive(panelConfirm,false);    
				
			}
		}
		else
		{
			mSubState = SUB_STATE_INPUT;
			Debug.Log("Tenten");
			if(panelMenu!=null)
			{

				NGUITools.SetActive(panelMenu,false);     
			}
			if(panelConfirm != null)
			{
				NGUITools.SetActive(panelConfirm,false);    

			}
		}
	}

	public static void EscapePress()
	{
		isConfirmShow = !isConfirmShow;
		NGUITools.SetActive(panelConfirm,isConfirmShow);     
		if(isConfirmShow)
		{
			NGUITools.SetActive(panelMenu,false);    
			NGUITools.SetActive(panelInput,false);    
		}
		else if(mSubState == SUB_STATE_INPUT)
		{
			NGUITools.SetActive(panelInput,true);    
			
		}
		else
			NGUITools.SetActive(panelMenu,true);    
	}
	#if UNITY_ANDROID
	public static void checkMyApp()
	{

        using (AndroidJavaClass jc = new AndroidJavaClass("com.hcg.baucua.tomcacop.UnityPlayerNativeActivity"))
        {
            jc.CallStatic<int>("checkMyApp", "com.hcg.baucua.tomcacop.UnityPlayerNativeActivity");
		}

	}
	#endif

}
