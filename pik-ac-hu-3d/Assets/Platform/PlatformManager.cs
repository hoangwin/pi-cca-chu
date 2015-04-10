using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class PlatformManager : MonoBehaviour
{


   
    public static PlatformManager instance;
    public Shader shaderHightLight;
    public Shader shaderNormal;
    public Shader shaderObjectHightLight;

    void Start()
    {

        instance = this;


        //	for (int i = 0; i < numberOfObjects; i++) {
        //		AddNewBlock();//Recycle();
        //	}


        //enabled = false;
    }

    void FixedUpdate()
    {
        //		Debug.Log (objectQueue.Count);

    }
}
	