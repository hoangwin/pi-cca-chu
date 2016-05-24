using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public int X;
    public int Y;
    public int Value;
    public GameObject objectBox;//box
    public GameObject renderrerObject;    
    public Vector3 fixPosition;//dung de reset lai tai vi tri ban dau

    //Map.Direction 
    void Movecompleted()
    {
        gameObject.SetActive(false);
        SoundEngine.play(SoundEngine.instance.paird);
        // willPaird.SetActive(false);
        
        Object obj = (Instantiate(GamePlay.instance.effect, GamePlay.instance.effect.transform.position, Quaternion.identity));
        Transform tran = (Transform)obj;


        tran.gameObject.SetActive(true);
        tran.position = this.gameObject.transform.position;

        this.gameObject.transform.position = fixPosition;
        GameObject.Destroy(renderrerObject);

        stopHint();
        MapCard.countCardLive--;
        if (MapCard.countCardLive <= 0)
        {
            GUIManager.state = GUIManager.STATE_OVER;
            //		Debug.Log("bbbbbbbbbbbbbbbbb");
            GUIManager.instance.mainMenu.SetActive(false);
            GUIManager.instance.ingameMenu.SetActive(false);
            GUIManager.instance.gameOver.SetActive(true);


            GUIManager.instance.textTitleOver.text = "Completed";
            SoundEngine.play(SoundEngine.instance.win);

            //
            int timePlay = (int)(GamePlay.instance.sliderbar.maxValue - GamePlay.instance.timeBegin);
            // Debug.Log("00:" + GamePlay.instance.sliderbar.maxValue);
            // Debug.Log("000:" + GamePlay.instance.timeBegin);
            if (timePlay <= 0)
                timePlay = 0;
            //Debug.Log("11:" + timePlay);
            GUIManager.instance.textCountOver.text = timePlay.ToString();
            if (MapCard.mode == 0)//easy
            {

                if (GamePlay.mScoreEasy.NUM > timePlay || GamePlay.mScoreEasy.NUM == 0)
                {
                    GamePlay.mScoreEasy.NUM = timePlay;
                    GamePlay.mScoreEasy.Save();
                }
                Debug.Log("222:" + GamePlay.mScoreEasy.NUM);
                GUIManager.instance.textBestCountTitleOver.text = "BEST TIME(EASY MODE)";
                GUIManager.instance.textBestCountOver.text = GamePlay.mScoreEasy.NUM.ToString();

            }
            if (MapCard.mode == 1)//easy
            {
                if (GamePlay.mScoreNormal.NUM > timePlay || GamePlay.mScoreEasy.NUM == 0)
                {
                    GamePlay.mScoreNormal.NUM = timePlay;
                    GamePlay.mScoreNormal.Save();
                }
                GUIManager.instance.textBestCountTitleOver.text = "BEST TIME(NORMAL MODE)";
                GUIManager.instance.textBestCountOver.text = GamePlay.mScoreNormal.NUM.ToString();
                GUIManager.instance.textCountOver.text = "NA";
            }
            if (MapCard.mode == 2)//easy
            {
                if (GamePlay.mScoreHard.NUM > timePlay || GamePlay.mScoreEasy.NUM == 0)
                {
                    GamePlay.mScoreHard.NUM = timePlay;
                    GamePlay.mScoreHard.Save();
                }
                GUIManager.instance.textBestCountTitleOver.text = "BEST TIME(HARD MODE)";
                GUIManager.instance.textBestCountOver.text = GamePlay.mScoreHard.NUM.ToString();
                GUIManager.instance.textCountOver.text = "NA";
            }
            GUIManager.ShowADS();


        }

    }
    public static void stopHint()
    {
        if (GamePlay.instance.isHint)
        {
            GamePlay.instance.isHint = false;
            GamePlay.instance.effectHint1.gameObject.SetActive(false);
            GamePlay.instance.effectHint2.gameObject.SetActive(false);

         //   GamePlay.instance.effectObject1.gameObject.GetComponent<Card>().objectBox.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderNormal;
          //  GamePlay.instance.effectObject2.gameObject.GetComponent<Card>().objectBox.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderNormal;

        }
    }
}
