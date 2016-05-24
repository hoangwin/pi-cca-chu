using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlay : MonoBehaviour
{
    public static bool isMove;
    public Transform tranformObjSelect;
    public Transform effect;
    public Transform effectBoxselect;
    public Transform effectHint1;
    public Transform effectHint2;
    public Transform effectObject1;
    public Transform effectObject2;

    public Transform objectMainMenu;
    
    public bool isHint;
    // Use this for initialization

    public Slider sliderbar;
    public float timeBegin = 60;
    public int mCountTimber = 0;

    public static int countHint;
    public static int countSort;
    // Use this for initialization
    public static SuperInt mScoreEasy = new SuperInt(0, "SCOREEasy");
    public static SuperInt mScoreNormal = new SuperInt(0, "SCORENormal");
    public static SuperInt mScoreHard = new SuperInt(0, "SCOREHard");

    bool isTouch;

    public static GamePlay instance;

    void Awake()
    {
        // Make the game run as fast as possible in the web player
        Application.targetFrameRate = 60;
    }
    void Start()
    {        
        isHint = false;
        instance = this;
        GamePlay.isMove = false;
        GamePlay.instance.effectHint1.gameObject.SetActive(false);
        GamePlay.instance.effectHint2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (GUIManager.state == GUIManager.STATE_PLAY)
        {
             sliderbar.value = timeBegin;
			
                timeBegin -= Time.deltaTime;
                if (timeBegin <= 0)
                {
                    GUIManager.state = GUIManager.STATE_OVER;
                    //		Debug.Log("bbbbbbbbbbbbbbbbb");
                    GUIManager.instance.mainMenu.SetActive(false);
                    GUIManager.instance.ingameMenu.SetActive(false);
                    GUIManager.instance.gameOver.SetActive(true);
                    GUIManager.instance.textTitleOver.text = "Game Over";
                    
                    if (MapCard.mode == 0)//easy
                    {//timeBegin
                        GUIManager.instance.textBestCountTitleOver.text = "BEST TIME(EASY MODE)";
                        GUIManager.instance.textBestCountOver.text = mScoreEasy.NUM.ToString();
                        GUIManager.instance.textCountOver.text = "NA";
                    }
                    if (MapCard.mode == 1)//easy
                    {
                        GUIManager.instance.textBestCountTitleOver.text = "BEST TIME(NORMAL MODE)";
                        GUIManager.instance.textBestCountOver.text = mScoreNormal.NUM.ToString();
                        GUIManager.instance.textCountOver.text = "NA";
                    }
                    if (MapCard.mode == 2)//easy
                    {
                        GUIManager.instance.textBestCountTitleOver.text = "BEST TIME(HARD MODE)";
                        GUIManager.instance.textBestCountOver.text = mScoreHard.NUM.ToString();
                        GUIManager.instance.textCountOver.text = "NA";
                    }

                    
                    SoundEngine.play(SoundEngine.instance.lose);
                    GUIManager.ShowADS();
                }
				


            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("aaaaaaaaaa");
                    //    hit.GetComponent<TouchObjectScript>().ApplyForce();

                }

            }
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    if (tranformObjSelect != null)
                    {

                        if(tranformObjSelect.gameObject.activeInHierarchy)
                            tranformObjSelect.Translate(0, -1, 0);
                        //tranformObjSelect.gameObject.GetComponent<Card>().objectBox.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderNormal;
                    }
                    //Debug.Log("x : " + hit.transform.gameObject.GetComponent<Card>().X + ", y : " + hit.transform.gameObject.GetComponent<Card>().Y + ", value : " + hit.transform.gameObject.GetComponent<Card>().Value);
                    hit.transform.Translate(0, 1, 0);
                    //  hit.transform.gameObject.GetComponent<Card>().objectBox.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderHightLight;
                    tranformObjSelect = hit.transform;
                    effectBoxselect.gameObject.SetActive(true);
                    effectBoxselect.position = tranformObjSelect.position;
                    effectBoxselect.transform.Translate(0, 0.2f, 0);
                    //    hit.GetComponent<TouchObjectScript>().ApplyForce();

                    MapCard.instance.CardClick(hit.transform.gameObject.GetComponent<Card>().X, hit.transform.gameObject.GetComponent<Card>().Y);
                  
                }
            }
        }
        
    }
}

