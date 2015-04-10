using UnityEngine;
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
    public bool isHint;
    // Use this for initialization
    Vector3 fingerPos;
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

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) )
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
                    tranformObjSelect.gameObject.GetComponent<Card>().renderrer.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderNormal;
                Debug.Log("x : " + hit.transform.gameObject.GetComponent<Card>().X + ", y : " + hit.transform.gameObject.GetComponent<Card>().Y + ", value : " + hit.transform.gameObject.GetComponent<Card>().Value);
                MapCard.instance.CardClick(hit.transform.gameObject.GetComponent<Card>().X, hit.transform.gameObject.GetComponent<Card>().Y);

                hit.transform.gameObject.GetComponent<Card>().renderrer.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderHightLight;
                tranformObjSelect = hit.transform;
                //    hit.GetComponent<TouchObjectScript>().ApplyForce();
            }
        }


        if (GUIManager.state == GUIManager.STATE_PLAY)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!GamePlay.isMove)
                {
                    GamePlay.isMove = true;
                    return;
                }
                SoundEngine.play(SoundEngine.instance.click);
                isTouch = true;
                fingerPos = Input.mousePosition;
                if (isTouch)
                {


                    if (fingerPos.x < (Screen.width / 2))
                    {
                        //     carLeft.gameObject.GetComponent<Runner>().ChangeLance();
                        //				    Debug.Log("1 :" + fingerPos);
                    }
                    else
                    {
                        //    carRight.gameObject.GetComponent<Runner>().ChangeLance();
                        //				Debug.Log("2 :" + fingerPos);
                    }

                    isTouch = false;

                }
            }
            else
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        if (!GamePlay.isMove)
                        {
                            GamePlay.isMove = true;
                            return;
                        }
                        SoundEngine.play(SoundEngine.instance.click);
                        isTouch = true;
                        fingerPos = Input.GetTouch(i).position;
                    }


                    if (isTouch)
                    {
                        if (fingerPos.x < (Screen.width / 2))
                        {
                            //   carLeft.gameObject.GetComponent<Runner>().ChangeLance();
                            //				    Debug.Log("1 :" + fingerPos);
                        }
                        else
                        {
                            //   carRight.gameObject.GetComponent<Runner>().ChangeLance();
                            //				Debug.Log("2 :" + fingerPos);
                        }

                        isTouch = false;

                    }
                }
            }

        }
    }
}

