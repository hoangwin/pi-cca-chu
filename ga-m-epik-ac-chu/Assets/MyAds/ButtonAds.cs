using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonAds : MonoBehaviour
{
    public  Button uibutton;
    public static int index = 0;
    public static float time;
    
    public static ButtonAds instance;
	// Use this for initialization
	void Start () {
        instance = this;
        setNewAds();
	}
	
	// Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2.5f)
        {
            setNewAds();
        }
    }
    public void setNewAds()
    {
        if (MyAds.isLoadText == true)
        {
            index++;
            if (index > (MyAds.MAX_ADS))
                index = 1;
            if (index == 1 && MyAds.sprite1 != null && uibutton != null && MyAds.isLoad[0])
            {
                time = 0;
                uibutton.image.sprite  = MyAds.sprite1;
                //uibutton.normalSprite2D = MyAds.sprite1;
            }
            else if (index == 2 && MyAds.sprite2 != null && uibutton != null && MyAds.isLoad[1])
            {
                time = 0;
                uibutton.image.sprite = MyAds.sprite2;
                //uibutton.normalSprite2D = MyAds.sprite2;
            }
            else if (index == 3 && MyAds.sprite3 != null && uibutton != null && MyAds.isLoad[2])
            {
                time = 0;
                uibutton.image.sprite = MyAds.sprite3;
                //uibutton.normalSprite2D = MyAds.sprite3;
            }
            else
            {
                time = 0;
                index = 0;
            }
        }
    }
    public void ButtonMoreGamePress()
    {

        //        SoundEngine.isSoundMusic = !SoundEngine.isSoundMusic;

        if (index == 0)
            Application.OpenURL("http://www.aegamemobile.com/");
        else
            Application.OpenURL(MyAds.AdsInfoArray[index - 1, 1]);
        /*if (SoundEngine.isSoundMusic)
        {
            MusicUiButton.normalSprite2D = music1Sprite;
            SoundEngine.getInstance().PlayLoop(SoundEngine.getInstance()._soundBG1);

        }

        else
        {
            MusicUiButton.normalSprite2D = music2Sprite;
            SoundEngine.getInstance().stopSound();
        }

        */
    }
}
