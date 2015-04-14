using UnityEngine;
using System.Collections;

public class SoundEngine : MonoBehaviour
{
    public static bool isSoundMusic = true;
    public static bool isSoundSFX = true;
    // Use this for initialization
   
    //music
    public AudioSource music;
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;

    //sfx
    public AudioClip lose;
	public AudioClip click;
	public AudioClip paird;
    public AudioClip win;
    public AudioClip failt;
    public AudioSource audioSource;
    
    public static SoundEngine instance;
    void Start()
    {

        instance = this;
    }
    public static void play(AudioClip clip)
    {
        instance.audioSource.PlayOneShot(clip);
    }
   
    // Update is called once per frame
    void Update()
    {

    }
}
