using UnityEngine;
using System.Collections;

public static class WP8Statics 	
{	

	//public delegate void MyDelegate(int num);
	//public static MyDelegate myDelegate;

	public static event System.EventHandler WP8FunctionHandle;//WP8FunctionHandle se la ham trong file xaml
	public static event System.EventHandler WP8FunctionHandle1;
	public static void SendSMS(string str)		
	{		
		if (WP8FunctionHandle != null)			
		{
			
			WP8FunctionHandle(str, null);            
			
		}
		
	}

	public static void StopAds(string str)		
	{		
		if (WP8FunctionHandle1 != null)			
		{
			
			WP8FunctionHandle1(str, null);            
			
		}
		
	}	
}
