using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {

	// Use this for initialization
	public static int STATE_MOVE_OPEN = 0;
	public static int STATE_MOVE_CLOSE = 1;	
	public static int STATE_IDE_OPEN = 2;
	public static int STATE_IDE_CLOSE = 3;
	public static int STATE_LAG = 4;
	public static int STATE_CHECK_MONEY = 5;

	public static int currentState = 0;
	public static int preState = 0;

	public static bool isConfirmShow = false;
	public static bool isConfirmNewCoinShow = false;

	public  GameObject panelGui = null;
	public  GameObject panelValue = null;
	public  GameObject panelConfirm = null;
	public  GameObject panelDiaLogGetNewCoin = null;
	public  GameObject panelDialogMoney = null;
	public  UILabel    labelDialogMoney = null;
	public  GameObject panelEffectResult = null;
	public  UILabel    labelButton1OpenCLose = null;

	public static string[] panelValuesName  = {"Bau","Cua","Tom","Ca","Ga","Nai"};
	public static int[] panelValuesCoin  = {5,5,5,5,5,5};

	public static int[] ValuesResultCoins  = {0,0,0,0,0,0};
	public  UILabel[] labelEffectResults = new UILabel[6];
	public GameObject[] panelValues = new GameObject[6];
	public UILabel[] labelValues = new UILabel[6];
	public BoxScript box1;
	public BoxScript box2;
	public BoxScript box3;

	public int mCoinAddValueIndex;
	public static int[] CoinAddValueArrayNonVip = {5,10,20};
	public static int[] CoinAddValueArrayVip = {5,10,20,50,100,200,500,1000};

	public static GamePlay instance;

	void Start () {
		GamePlay.instance = this;
		mCoinAddValueIndex = 0;
		if(panelConfirm == null)
			initAllObject();
		//
		currentState = STATE_MOVE_OPEN;
		//PlateScript plate = (PlateScript) GameObject.Find("Plate").GetComponent("PlateScript");
		//plate.setState(STATE_IDE_OPEN);
		DEF.Init();
		GameObject hand = GameObject.Find("BackGround");
		//GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity,new  Vector3(DEF.scaleX, DEF.scaleY, 1));

		if(hand!= null)
		{
			//	DEF.ResizeBySize(hand,DEF.DISPLAY_WIDTH,DEF.DISPLAY_HEIGHT);
			hand.transform.localScale = new Vector3 (DEF.sx_ortho, DEF.sy_ortho, 1);
		}

		disableAllInPanelValue();
		if(panelConfirm != null)
		{
			NGUITools.SetActive(panelConfirm, false); 
			//NGUITools.SetActive(panelDialogMoney,false); 
			NGUITools.SetActive(panelDiaLogGetNewCoin,false); 
		}
		DEF.ScaleAnchorGui();
	}
	public void initAllObject()
	{
		panelConfirm = GameObject.Find("PanelConFirm");
		panelGui = GameObject.Find("PanelGui");
		panelValue = GameObject.Find("PanelValue");
		panelDialogMoney = GameObject.Find("PanelDialogMoney");
		panelEffectResult = GameObject.Find("PanelEffectResult");

		labelDialogMoney = GameObject.Find("LabelDialogMoney").GetComponent<UILabel>(); 
		//user name and coin
		UILabel usernameLabel = GameObject.Find("LabelUserName").GetComponent<UILabel>(); 
		UILabel coinLabel = GameObject.Find("LabelCoin").GetComponent<UILabel>(); 
		usernameLabel.text = SaveLoadData.UserName;
		coinLabel.text = CoinScore.getRealCoin().ToString();

		panelDiaLogGetNewCoin = GameObject.Find("PanelDiaLogGetNewCoin");

		box1 =(BoxScript) GameObject.Find("Box1").GetComponent("BoxScript");
		box2 =(BoxScript) GameObject.Find("Box2").GetComponent("BoxScript");
		box3 =(BoxScript) GameObject.Find("Box3").GetComponent("BoxScript");


		if(SaveLoadData.UserName.Length<=5)
			NGUITools.SetActive(GameObject.Find("Button5AddCoin"),false);	
		#if UNITY_WP8
		else
		{
			int expDate = 20140121;
			int date = int.Parse(System.DateTime.Now.ToString("yyyyMMdd"));
			if (expDate > date) {
				NGUITools.SetActive(GameObject.Find("Button5AddCoin"),false);	
				Debug.Log("Date : " + date);
			}  

			Debug.Log("Tick: " + System.DateTime.Now.Ticks);
		}
		#endif

		labelButton1OpenCLose = GameObject.Find("LabelButton1OpenCLose").GetComponent<UILabel>(); 
		
		for(int i=0;i<6;i++)
		{
			panelValues[i] = GameObject.Find("Panel"+ panelValuesName[i]); 			
			labelValues[i] = GameObject.Find("Label" + panelValuesName[i]).GetComponent<UILabel>();
			//
			labelEffectResults[i] =GameObject.Find("LabelEffectResult" + panelValuesName[i]).GetComponent<UILabel>();
			labelEffectResults[i].text =" ";				
		}
	}
	public void disableAllInPanelValue()
	{
		for(int i=0;i<6;i++)
		{
			labelValues[i].text = "0";//panelValues[i].GetComponent<UILabel>().text = "0";			
			NGUITools.SetActive(GamePlay.instance.panelValues[i],false);

			panelValuesCoin[i] = 0;
			labelValues[i].text = "0";

			labelEffectResults[i].text =" ";		
		}
	}
	public void setTextCoin()
	{
		UILabel coinLabel = GameObject.Find("LabelCoin").GetComponent<UILabel>(); 
		coinLabel.text = CoinScore.getRealCoin().ToString();
	}

	public void setActivePanelGui( bool active)
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("MyTag_ButtonGui");
		for(int i =0;i<objects.Length;i++)
		{
			//objects[i].collider.isTrigger = active;
			objects[i].GetComponent<Collider>().enabled = active;
			Debug.Log("objects[i] :" +objects[i]);
		}
	}
	public void checkMoneyWhenOpen()
	{
		for(int i = 0;i<6;i++)
		{
			ValuesResultCoins[i]=0;
			if(box1.value == i && panelValuesCoin[i]>0)
			{
				ValuesResultCoins[i] += panelValuesCoin[i];
			}
			if(box2.value == i&& panelValuesCoin[i]>0)
			{
				ValuesResultCoins[i] += panelValuesCoin[i];
			}
			if(box3.value == i&& panelValuesCoin[i]>0)
			{
				ValuesResultCoins[i] +=  panelValuesCoin[i];
			}
			if(ValuesResultCoins[i] >0)
				labelEffectResults[i].text = "+"+ ValuesResultCoins [i];
			else 
				labelEffectResults[i].text = " ";
		}
		for(int i = 0;i<6;i++)
		{
			if(ValuesResultCoins[i] >0)
				CoinScore.setOffsetCoin(CoinScore.getOffsetCoin() + ValuesResultCoins[i] + panelValuesCoin[i]);
		}
		setTextCoin();

		SaveLoadData.saveGame();
	}
	public void checkOpenDialogAddNewCOin()
	{
		Debug.Log("awwwwwwwww");
		if(CoinScore.getRealCoin() < 5)
		{
			CoinScore.setDefaultValue();
			NGUITools.SetActive(GamePlay.instance.panelDialogMoney,true);
			if(GamePlay.instance.panelConfirm != null)
			{
				isConfirmNewCoinShow = true;
				NGUITools.SetActive(GamePlay.instance.panelConfirm,false);
				NGUITools.SetActive(GamePlay.instance.panelDiaLogGetNewCoin,true);
				GamePlay.instance.setActivePanelGui(false);
				
			}
			setTextCoin();
			//GamePlay.instance.labelDialogMoney.text = "Bạn vừa được tặng 200 Coin";
		}	
	}
	// Update is called once per frame
	void Update () {
		//Debug.Log(currentState);
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(isConfirmShow)
			{
				GamePlay.isConfirmShow = false;
				NGUITools.SetActive(GamePlay.instance.panelConfirm,false);     
				//	NGUITools.SetActive(GamePlay.panelValue,true);     
				//	NGUITools.SetActive(GamePlay.panelGui,true);     
				GamePlay.instance.setActivePanelGui(true);
			}
			else if(isConfirmNewCoinShow)
			{
				ButtonGamePlay.instance.YesNewCoinConfirmPress();
			}
			else	
			{
				isConfirmShow = true;
				NGUITools.SetActive(panelConfirm,true);
				setActivePanelGui(false);			
			}
		}
	}
}
