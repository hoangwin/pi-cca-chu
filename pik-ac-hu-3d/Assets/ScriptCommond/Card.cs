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
    public GameObject renderrer;//box
    public GameObject renderrerObject;
    public GameObject willPaird;
    public Vector3 fixPosition;//dung de reset lai tai vi tri ban dau
   
    //Map.Direction 
    void Movecompleted()
    {
        gameObject.SetActive(false);
        willPaird.SetActive(false);
        
        Object obj = (Instantiate(GamePlay.instance.effect, GamePlay.instance.effect.transform.position, Quaternion.identity));
        Transform tran = (Transform)obj;
        

        tran.gameObject.SetActive(true);
        tran.position = this.gameObject.transform.position;

        this.gameObject.transform.position = fixPosition;

       if(  GamePlay.instance.isHint)
       {
           GamePlay.instance.isHint = false;
            GamePlay.instance.effectHint1.gameObject.SetActive(false);
            GamePlay.instance.effectHint2.gameObject.SetActive(false);
            
            GamePlay.instance.effectObject1.gameObject.GetComponent<Card>().renderrer.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderNormal;
            GamePlay.instance.effectObject2.gameObject.GetComponent<Card>().renderrer.GetComponent<Renderer>().material.shader = PlatformManager.instance.shaderNormal;            
            
       }
    }
}
