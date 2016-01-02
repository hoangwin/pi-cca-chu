using UnityEngine;
using System.Collections;

public class ButtonRanking : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void EscapePress()
	{
		SoundEngine.playSounBack(this);
		Debug.Log("Back from Help");
		if(Ranking.isMoveFromMainmenu)
				Application.LoadLevel("MainMenu");
		else
			Application.LoadLevel("GamePlay");
	}





}
