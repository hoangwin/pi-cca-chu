using UnityEngine;
using System.Collections;

public class ButtonHelp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void EscapePress()
	{
		Debug.Log("Back from Help");
		Application.LoadLevel("MainMenu");
	}





}
