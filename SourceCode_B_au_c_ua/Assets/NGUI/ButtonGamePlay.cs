using UnityEngine;
using System.Collections;

public class ButtonGamePlay : MonoBehaviour {
		public static ButtonGamePlay instance = null;
		void Start () {
		//	GameObject mobject =  GamePlay.instanceGameObject.Find("PanelBau");
		//	NGUITools.SetActive(GamePlay.panelValue,false); 
		//GamePlay.panelValue.
		instance = this;
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		void MainmenuButtonPress()
		{
			SoundEngine.playSounBack(this);
			/*if(panelGui != null)
			NGUITools.SetActive(panelGui,false);

		if(panelValue != null)
			NGUITools.SetActive(panelValue,false);
		*/
			if(GamePlay.instance.panelConfirm != null)
			{
				GamePlay.isConfirmShow = true;
				NGUITools.SetActive(GamePlay.instance.panelConfirm,true);
				GamePlay.instance.setActivePanelGui(false);
			}

			
		}
		void RankingButtonPress()
		{
			SoundEngine.playSounBack(this);
			Ranking.isMoveFromMainmenu = false;
			Application.LoadLevel("Ranking");
			
		}
		
			
		void YesConfirmPress()
		{
			Application.LoadLevel("MainMenu");
			
		}
		void NoConfirmPress()
		{
			GamePlay.isConfirmShow = false;
			NGUITools.SetActive(GamePlay.instance.panelConfirm,false);     
		//	NGUITools.SetActive(GamePlay.panelValue,true);     
		//	NGUITools.SetActive(GamePlay.panelGui,true);     
			GamePlay.instance.setActivePanelGui(true);
				
			
		}
		public void YesNewCoinConfirmPress()
		{
			GamePlay.isConfirmNewCoinShow = false;
			CoinScore.setDefaultValue();
			GamePlay.instance.setTextCoin();
			GamePlay.instance.setActivePanelGui(true);
			NGUITools.SetActive(GamePlay.instance.panelConfirm,false);   
			NGUITools.SetActive(GamePlay.instance.panelDiaLogGetNewCoin,false);   

		}

		void ButtonOpenCloseClick()
		{
			
			Debug.Log("GamePlay.currentState :" + GamePlay.currentState);
			if(GamePlay.currentState == GamePlay.STATE_IDE_OPEN)// click de dong = 2 
			{
				GamePlay.instance.disableAllInPanelValue();

				GamePlay.currentState = GamePlay.STATE_MOVE_CLOSE;//1
					PlateScript plate =(PlateScript) GameObject.Find("Plate").GetComponent("PlateScript");
					plate.setState(GamePlay.currentState);
			}
			if(GamePlay.currentState == GamePlay.STATE_IDE_CLOSE) //click de mo = 3
			{
					GamePlay.currentState =  GamePlay.STATE_MOVE_OPEN;//0
					PlateScript plate =(PlateScript) GameObject.Find("Plate").GetComponent("PlateScript");
					plate.setState(GamePlay.currentState);
			}
				//else if(GamePlay.currentState == 2)
				//{
				//	GamePlay.currentState++;
				//	PlateScript plate =(PlateScript) GameObject.Find("Plate").GetComponent("PlateScript");
			//		plate.setState(GamePlay.currentState);
			//	}
			
		}
		void AddCoinButtonPress()
		{
			Application.LoadLevel("AddCoin");
		}

		void ChooseCoinButtonPress()
		{
			
			if(SaveLoadData.isVipUser == 0)
			{
				GamePlay.instance.mCoinAddValueIndex++;
				if(GamePlay.instance.mCoinAddValueIndex>= GamePlay.CoinAddValueArrayNonVip.Length)
				{
					GamePlay.instance.mCoinAddValueIndex = 0;
				}
			}
			else
			{
				GamePlay.instance.mCoinAddValueIndex++;
				if(GamePlay.instance.mCoinAddValueIndex>= GamePlay.CoinAddValueArrayVip.Length)
				{
					GamePlay.instance.mCoinAddValueIndex = 0;
				}
			}
			GameObject.Find("LabelChooseCoin").GetComponent<UILabel>().text =""+GamePlay.CoinAddValueArrayVip[GamePlay.instance.mCoinAddValueIndex];		
		}
	#region " Source Code Button In board"

		void addPointWhenClickBoard(int i)
		{
			int sum=0;
			for(int j =0 ;j< 6;j++)
			{
				sum += GamePlay.panelValuesCoin[j];
			}
			sum += GamePlay.CoinAddValueArrayVip[GamePlay.instance.mCoinAddValueIndex];

			if(sum >100 && SaveLoadData.isVipUser == 0)
			{
				GamePlay.instance.labelDialogMoney.text = "Tổng tiền đặt tối đa 100 coin.";
				return;
			}
			if(sum > 50000 && SaveLoadData.isVipUser != 0)
			{
				GamePlay.instance.labelDialogMoney.text = "Tổng tiền đặt tối đa 50.000 coin.";
				return;
			}
			if(CoinScore.getRealCoin() < GamePlay.CoinAddValueArrayVip[GamePlay.instance.mCoinAddValueIndex])
			{
				GamePlay.instance.labelDialogMoney.text = "Bạn có ít hơn "+GamePlay.CoinAddValueArrayVip[GamePlay.instance.mCoinAddValueIndex]+ " coin.";
				NGUITools.SetActive(GamePlay.instance.panelDialogMoney,true);
				return;
			}
			else
			{
				GamePlay.instance.labelDialogMoney.text = "Chúc Bạn May Mắn";
			}

			if(GamePlay.currentState == GamePlay.STATE_IDE_CLOSE)
			{
				SoundEngine.playSounDatCoin(this);
			Debug.Log("i : "+i);
				NGUITools.SetActive(GamePlay.instance.panelValues[i],true);
					//GamePlay.labelValues[0].GetComponent<UILabel>();
			GamePlay.panelValuesCoin[i] += GamePlay.CoinAddValueArrayVip[GamePlay.instance.mCoinAddValueIndex];	
				GamePlay.instance.labelValues[i].text = "" + GamePlay.panelValuesCoin[i];
				//data
			CoinScore.addScore(-GamePlay.CoinAddValueArrayVip[GamePlay.instance.mCoinAddValueIndex]);				
				GamePlay.instance.setTextCoin();
				
			}
			else if(GamePlay.currentState == GamePlay.STATE_IDE_OPEN)
			{
				GamePlay.instance.disableAllInPanelValue();
				
				GamePlay.currentState = GamePlay.STATE_MOVE_CLOSE;//1
				PlateScript plate =(PlateScript) GameObject.Find("Plate").GetComponent("PlateScript");
				plate.setState(GamePlay.currentState);
			}

		}
		void ButtonBauClick()
		{
			addPointWhenClickBoard(0);
		}
		void ButtonCuaClick()
		{
			addPointWhenClickBoard(1);
		}
		void ButtonTomClick()
		{
			addPointWhenClickBoard(2);
		}

		void ButtonCaClick()
		{
			addPointWhenClickBoard(3);
		}

		void ButtonGaClick()
		{
			addPointWhenClickBoard(4);
		}

		void ButtonNaiClick()
		{
			addPointWhenClickBoard(5);
		}
	#endregion

	}
