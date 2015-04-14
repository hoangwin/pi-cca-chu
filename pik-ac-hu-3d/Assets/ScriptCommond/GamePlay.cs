using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlay : MonoBehaviour
{
    public static bool isMove;
    public Transform tranformObjSelect;
    public Transform effect;
    public Transform effectHint1;
    public Transform effectHint2;
    public Transform effectObject1;
    public Transform effectObject2;

    public Transform objectMainMenu;
    public Transform objectInGame;
    public bool isHint;
    // Use this for initialization

    public Slider sliderbar;
    public float timeBegin = 60;
    public int mCountTimber = 0;

    public static int countHint;
    public static int countSort;
    // Use this for initialization
    

    
    
    
    bool isTouch;

    public static GamePlay instance;

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
                    GUIManager.instance.textCountOver.text = "NA";
                    GUIManager.instance.textBestCountOver.text = "10";
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
                        tranformObjSelect.gameObject.GetComponent<Card>().objectBox.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderNormal;
                    Debug.Log("x : " + hit.transform.gameObject.GetComponent<Card>().X + ", y : " + hit.transform.gameObject.GetComponent<Card>().Y + ", value : " + hit.transform.gameObject.GetComponent<Card>().Value);
                    MapCard.instance.CardClick(hit.transform.gameObject.GetComponent<Card>().X, hit.transform.gameObject.GetComponent<Card>().Y);

                    hit.transform.gameObject.GetComponent<Card>().objectBox.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderHightLight;
                    tranformObjSelect = hit.transform;
                    //    hit.GetComponent<TouchObjectScript>().ApplyForce();
                }
            }
        }
        
    }
}

