using UnityEngine;
using System.Collections;

public class PlateScript : MonoBehaviour {

	// Use this for initialization
	Animator anim;
	public int currentState = 2;

	public static int STATE_MOVE_OPEN = 0;
	public static int STATE_MOVE_CLOSE = 1;	
	public static int STATE_IDE_OPEN = 2;
	public static int STATE_IDE_CLOSE = 3;
	public static PlateScript instance =null;


	void Start () {
		anim = gameObject.GetComponent<Animator>();
		instance = this;

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setState(int state)
	{
		currentState = state;
		anim.SetInteger("State",state);
	}

	public void AnimFinish(int _currentState)
	{

		Debug.Log("State change :" + _currentState);

		if(_currentState < 4)
		{
			GamePlay.currentState = _currentState;//_currentState;
			setState(_currentState);
		}
		if(_currentState == GamePlay.STATE_IDE_OPEN)
		{
			GamePlay.instance.labelButton1OpenCLose.text="Xốc";

			GamePlay.instance.checkMoneyWhenOpen();
			SoundEngine.playSounOpen(this);
		}
		if(_currentState == GamePlay.STATE_IDE_CLOSE)
		{
			GamePlay.instance.labelButton1OpenCLose.text="Mở";
			//BoxScript box1 =(BoxScript) GameObject.Find("Box1").GetComponent("BoxScript");
			//BoxScript box2 =(BoxScript) GameObject.Find("Box2").GetComponent("BoxScript");
			//BoxScript box3 =(BoxScript) GameObject.Find("Box3").GetComponent("BoxScript");

			int value1 = (int)(Random.Range(0,6));
			if(value1 >=6) value1 = 5;

			int value2 = (int)(Random.Range(0,6));
			if(value2 >=6) value2 = 5;

			int value3 = (int)(Random.Range(0,6));
			if(value3 >=6) value3 = 5;
			GamePlay.instance.box1.setValue(value1);
			GamePlay.instance.box2.setValue(value2);
			GamePlay.instance.box3.setValue(value3);
		
			GameObject.Find("Box1").transform.localRotation  =Quaternion.Euler( new Vector3(0,0,Random.Range(0,360)));
			SoundEngine.playBowlLag(this);
			GamePlay.instance.checkOpenDialogAddNewCOin();

		}


	}

}
