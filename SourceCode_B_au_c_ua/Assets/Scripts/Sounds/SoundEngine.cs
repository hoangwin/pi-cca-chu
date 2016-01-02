using UnityEngine;
using System.Collections;

public class SoundEngine : MonoBehaviour {
	public static GameObject SoundBG = null;
	public static GameObject SoundClickButton = null;
	public static bool isSoundEnable = true;

	public static string PATH_FREFAB = "";
	public static void initSound()
	{
			//SoundBG = (GameObject)(Instantiate(GameEngine.frefabItem));
		//SoundBG = (GameObject)Instantiate(Resources.Load("/Assets/BGMusic.prefab"));
		//SoundBG = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/BGMusic.prefab", GameObject));
		//SoundBG = (GameObject)(GameObject.Instantiate( Resources.LoadAssetAtPath(PATH_FREFAB + "BGMusic.prefab", typeof(GameObject)) ) );
		if(SoundBG == null)
		{//load tu Resource folder
			SoundBG = (GameObject)(GameObject.Instantiate( Resources.Load(PATH_FREFAB + "Sounds/BGMusic", typeof(GameObject)) ) );

			DontDestroyOnLoad(SoundBG);
		}
		if(SoundClickButton == null)
		{//cai nay da co san tren scence
			SoundClickButton = GameObject.Find("SoundButton");
			DontDestroyOnLoad(SoundClickButton);
			
		}
	}

	void Awake()
	{
		/*
		Debug.Log("TOAN STT");
		GameObject gameMusic = GameObject.Find("BGMusic");
		
		if (gameMusic) {
			if (!gameMusic.audio.isPlaying)
				if (SoundEngine.isSoundEnable)
					gameMusic.audio.Play();
		}
	
		
		GameObject selectSound = GameObject.Find("EffectMusic");
		DontDestroyOnLoad(selectSound);
		*/
	}
	public static void playSounBack(MonoBehaviour scene)
	{
		if (!SoundEngine.isSoundEnable)
			return;

		if (SoundClickButton != null)
		{
			// Debug.Log("Play Sound");
			SoundClickButton.GetComponent<AudioSource>().Play();
		}
	}

	public static void playSounDatCoin(MonoBehaviour scene)
	{
		if (!SoundEngine.isSoundEnable)
			return;
		GameObject sound = GameObject.Find("Sound_Coin");
		if (sound != null)
		{
			// Debug.Log("Play Sound");
			sound.GetComponent<AudioSource>().Play();
		}
	}
	public static void playSounOpen(MonoBehaviour scene)
	{
		if (!SoundEngine.isSoundEnable)
			return;
		GameObject sound = GameObject.Find("Sound_Open");
		if (sound != null)
		{
			// Debug.Log("Play Sound");
			sound.GetComponent<AudioSource>().Play();
		}
	}

	public static void playBowlLag(MonoBehaviour scene)
	{
		if (!SoundEngine.isSoundEnable)
			return;
		GameObject sound = GameObject.Find("Sound_BowlLag");
		if (sound != null)
		{
			// Debug.Log("Play Sound");
			sound.GetComponent<AudioSource>().Play();
		}
	}

	public static void OptionSoundClick()
	{
		SoundEngine.isSoundEnable = !SoundEngine.isSoundEnable;
		if(SoundEngine.isSoundEnable)
		{   
			if (SoundBG) {
				if (!SoundBG.GetComponent<AudioSource>().isPlaying)
					SoundBG.GetComponent<AudioSource>().Play();
			}
			
		}
		else
		{
			
			if (SoundBG) {
				if (SoundBG.GetComponent<AudioSource>().isPlaying)
					SoundBG.GetComponent<AudioSource>().Stop();
			}
		}

	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
