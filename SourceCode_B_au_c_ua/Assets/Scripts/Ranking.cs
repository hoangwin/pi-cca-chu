using UnityEngine;
using System.Collections;

public class Ranking : MonoBehaviour {
	public WWW www;
	public static bool loadRanking = true;
	public static bool isMoveFromMainmenu = false;
	public GameObject[] listUserObject = new GameObject[11];
	void Start () {
		DEF.Init();
		GameObject hand = GameObject.Find("BackGround");
		//GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity,new  Vector3(DEF.scaleX, DEF.scaleY, 1));
		
		if(hand!= null)
		{
			//	DEF.ResizeBySize(hand,DEF.DISPLAY_WIDTH,DEF.DISPLAY_HEIGHT);
			hand.transform.localScale = new Vector3 (DEF.sx_ortho, DEF.sy_ortho, 1);
		}
	//	child=transform.Find("cirkel");
		for(int i=0;i<11;i++)
		{
			listUserObject[i]= GameObject.Find("User"+ (i+1));
			listUserObject[i].transform.Find("Label2Name").GetComponent<UILabel>().text="aaa";
		}
		PostHightScore();
		getHightScore();
		DEF.ScaleAnchorGui();
		loadRanking = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			EscapePress();

		}
		getHightScoreDone();
	}
	public static void EscapePress()
	{
		Debug.Log("Back from Help");
		if(isMoveFromMainmenu)
			Application.LoadLevel("MainMenu");
		else
			Application.LoadLevel("GamePlay");
	}

	public void  getHightScore()
	{
		www = new WWW("http://gamethuanviet.com/baucuatomca/SetGetData.php?type=select&username="+ SaveLoadData.UserName);
		Debug.Log("http://gamethuanviet.com/baucuatomca/SetGetData.php?type=select&username=" + SaveLoadData.UserName);


	}
	public void  getHightScoreDone()
	{
		if(loadRanking && www.isDone)
		{
			loadRanking = false;
			Debug.Log(www.text);
			string[] strarray;
			strarray = www.text.Split('|');
			if (strarray.Length > 30)
			{
				for (int i = 0; i < 10; i++)
				{
					
					listUserObject[i] = GameObject.Find("User"+ (i+1));
					listUserObject[i].transform.Find("Label2Name").GetComponent<UILabel>().text = strarray[i * 3 + 1];
					listUserObject[i].transform.Find("Label3Money").GetComponent<UILabel>().text = strarray[i * 3 + 2];
					
					
					//scoreLabel[i] = strarray[i * 3 + 2];
				}
			}
			if (strarray.Length > 33)
			{
				listUserObject[10].transform.Find("Label2Pos").GetComponent<UILabel>().text= strarray[10 * 3 + 1];
				listUserObject[10].transform.Find("Label2Name").GetComponent<UILabel>().text = strarray[10 * 3 + 2];
				listUserObject[10].transform.Find("Label3Money").GetComponent<UILabel>().text = strarray[10 * 3 + 3];
			}
		}
	}
	public static void PostHightScore()
	{
		//http://gamethuanviet.com/baucuatomca/SetGetData.php?type=update&username=%s&Score=%d&Level=0&Played=0&country=NA "
		string strPost = "http://gamethuanviet.com/baucuatomca/SetGetData.php?type=update&username=" + SaveLoadData.UserName + "&Score=" + CoinScore.getRealCoin() +"&Level=0&Played=0&country=NA";
		Debug.Log(strPost);
		WWW www = new WWW(strPost);
	}
}
