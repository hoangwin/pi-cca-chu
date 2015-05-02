using UnityEngine;
using System.Collections;

public class CameraFllow : MonoBehaviour {
	
    public bool is3D;
    public static CameraFllow instance;
	// Use this for initialization
	void Start () {
	//	destination = target.transform.position;
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void LateUpdate() {
		
		{
		//	destination = new Vector3 (0,
		//                                      target.transform.position.y + yOffset,
	//		                           target.transform.position.z + zOffset);
		//	transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, 0.2f);
		}
	}
    public void setCameraDirection(bool _is3D)
    {
        is3D = _is3D;
        if(is3D)
        {
          
           
        }
        else
        {
           
        }
        
    }



}
