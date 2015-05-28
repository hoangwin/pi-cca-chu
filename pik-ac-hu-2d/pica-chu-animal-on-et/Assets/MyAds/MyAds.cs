using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MyAds : MonoBehaviour {

    //public static Image image1;
    public static Sprite sprite1;
    public static Sprite sprite2;
    public static Sprite sprite3;

    
    public delegate void HandleCompleted();
    public delegate void HandleFailed();
    public static HandleCompleted onCompleted;
    public static HandleFailed onFailed;
    public static string[,] AdsInfoArray = new string[3, 3];// toi da la 3 quang cao
    public static bool[] isLoad = { false, false, false };
    public static bool isFistTime = false;
    //huong dan
    //AdsInfoArray[0,x] = quang cao 1
    //AdsInfoArray[0,0] = Ten Quang cao 1
    //AdsInfoArray[0,1] = Link Quang cao 1
    //AdsInfoArray[0,2] = Link Hinh Quang cao 1
    
    public static string STR_LOAD;
    public static int MAX_ADS = 0;

    public static string Link = "http://aegamemobile.com/MyAdsControl/index.php?GAME=BAN_TRUNG_KHUNG_LONG_THUANVIETGAME&PLATFORM=";
    //http://aegamemobile.com/MyAdsControl/index.php?GAME=BUBBLE_SHOOT_FREE&PLATFORM=IOS
    public static bool isLoadText = false;
    
    public static MyAds instance;
	void Start () {

        if (instance == null)
        {           
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
        if (!isLoadText)
        {
            GetADS();
        }
	}
	
	// Update is called once per frame
	
    public static void GetADS()
    {
#if UNITY_ANDROID
        GetHTML(Link+"ANDROID", (HandleCompleted)OnLoadDone);
#elif UNITY_WP8
        GetHTML(Link+"WP8", (HandleCompleted)OnLoadDone);

#elif UNITY_IOS
        GetHTML(Link+"IOS", (HandleCompleted)OnLoadDone);

#endif


    }
    public static void GetHTML(string uri, HandleCompleted c)
    {
        onCompleted = null;
        onCompleted += c;
        //onFailed += f;
       instance.StartCoroutine(LoadPageByWWW(uri, onCompleted));
    }
   static IEnumerator LoadPageByWWW(string url, HandleCompleted onCompleted)
    {
        Debug.Log(url);
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            STR_LOAD = www.text;
            Debug.Log("OK");
            onCompleted();
        }
        else
        {
            STR_LOAD = null;
            Debug.Log("error");
            onCompleted();
        }
    }
    public static void OnLoadDone()
    {
        isLoadText = true;
        string STR = STR_LOAD;
        // STR = STR_LOAD = " adad d |dad |adadad \r\ndasda|ds|";//test loi
        Debug.Log(STR_LOAD);
        if (STR == null || STR.Length < 5) return;
        STR = STR.Replace("\r\n", "");
        STR = STR.Replace("\r", "");
        STR = STR.Replace("\n", "|");
        string[] strArray = STR.Split('|');
        
        MAX_ADS = strArray.Length / 3;
        for (int i = 0; i < MAX_ADS; i++)
        {
            AdsInfoArray[i, 0] = strArray[i * 3];
            AdsInfoArray[i, 1] = strArray[i * 3 +1];
            AdsInfoArray[i, 2] = strArray[i * 3 + 2];
//            Debug.Log(AdsInfoArray[i, 0]);
        }
        //LoadImage("http://clipsquangcao.com/ads/ads_thuthanh.png");
       LoadImage();
        
       
    }
    public static void LoadImage()
    {
        for (int i = 0; i < MAX_ADS; i++)
        {
            if(!isLoad[i])
                instance.StartCoroutine(LoadImage(AdsInfoArray[i, 2], i + 1));            
           // Debug.Log(AdsInfoArray[i, 0]);
        }
      
       
      
    }
    public static IEnumerator LoadImage(string imageUrl, int _index)
    {

        WWW loader = new WWW(imageUrl);
        yield return loader;
        if (loader.error == null)
        {
            int w = loader.texture.width;
            int h = loader.texture.height;
            
            if (_index == 1)
            {
                Debug.Log(1);
                
                sprite1 = Sprite.Create(loader.texture, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f), 1);
                isLoad[0] = true;              
                
            }
            else if (_index == 2)
            {
                Debug.Log(2);
                sprite2 = Sprite.Create(loader.texture, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f), 1);
                isLoad[1] = true;
               
            }
            else if (_index == 3)
            {
                Debug.Log(3);
                sprite3 = Sprite.Create(loader.texture, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f), 1);
                isLoad[2] = true;
                
            }
            if (!isFistTime)
            {
                isFistTime = true;
                ButtonAds.index = _index - 1;// de qua kia ++ -> hop li
                ButtonAds.time = 0;
                ButtonAds.instance.setNewAds();
            }
         //   if (_index == MAX_ADS)
         //       isLoad = true;
           // SPRITE.sprite2D = SPRITEMP;
           // TADS.GetComponent<UIButton>().normalSprite2D = SPRITEMP;
           // LABEL.text = "";
        }
        else
        {
            Debug.Log("LoadImage hinh bi loi");
           // SPRITEMP = null;
           // LABEL.text = TEXT;
        }

    }
    static void OnLoadFailed()
    {

    }

    
}
