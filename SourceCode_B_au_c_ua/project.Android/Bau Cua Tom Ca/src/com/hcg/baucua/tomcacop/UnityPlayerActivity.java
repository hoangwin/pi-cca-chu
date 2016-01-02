package com.hcg.baucua.tomcacop;

import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdSize;
import com.google.android.gms.ads.AdView;
import com.unity3d.player.*;

import android.app.Activity;
import android.app.NativeActivity;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.telephony.SmsManager;
import android.util.Log;
import android.view.Gravity;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Toast;
import android.widget.FrameLayout;
import android.widget.LinearLayout;

public class UnityPlayerActivity extends Activity
{
	protected UnityPlayer mUnityPlayer; // don't change the name of this variable; referenced from native code
	public static UnityPlayerActivity instance;
	// UnityPlayer.init() should be called before attaching the view to a layout - it will load the native code.
	// UnityPlayer.quit() should be the last thing called - it will unload the native code.
	public static String SAVE_REF ="SAVE_FILE";
	public static String SAVE_IS_ADS ="SAVE_FILE";
	public static boolean isShowAds = true;
	@Override protected void onCreate (Bundle savedInstanceState)
	{
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);

		getWindow().setFormat(PixelFormat.RGBX_8888); // <--- This makes xperia play happy

		mUnityPlayer = new UnityPlayer(this);
		View playerView = mUnityPlayer.getView();
	//	setContentView(playerView);
		playerView.requestFocus();
		instance = this;
		layout = new FrameLayout(this);
		layout.setPadding(0, 0, 0, 0);
		loadGame();
		//showAdmobAds( this);
	
		layout.addView(playerView);
					
		setContentView(layout);		
	}


	static FrameLayout layout ;
	static FrameLayout.LayoutParams adsParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WRAP_CONTENT, FrameLayout.LayoutParams.WRAP_CONTENT, android.view.Gravity.BOTTOM | android.view.Gravity.CENTER);
	//public static LinearLayout layout;
	public static AdView adView ;
	public static boolean isloadAds = false;
	
	public static void showAdmobAds( final UnityPlayerActivity unityPlayerActivity)
	{
		
		Log.v("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
			    adView = new AdView(UnityPlayer.currentActivity);
			    adView.setAdSize(AdSize.BANNER);
			    adView.setAdUnitId("ca-app-pub-1521173422394011/9030376489");
			    
			//	adView = new AdView(UnityPlayer.currentActivity, AdSize.SMART_BANNER, "a1531e034cf3eee");//hcgmobilegame
				
				 AdRequest adRequest = new AdRequest.Builder()
			       // .addTestDevice(AdRequest.DEVICE_ID_EMULATOR)
			      //  .addTestDevice("INSERT_YOUR_HASHED_DEVICE_ID_HERE")
			        .build();
				 adView.setAdListener(new AdListener() {
					  @Override
					  public void onAdLoaded() {
						  if(isloadAds == false)
						  {
							  layout.addView(adView,adsParams);
							  isloadAds = true;
						  }
					  }
					});
				adView.loadAd(adRequest);	
			}
		});	
	}
	public static void checkMyApp(String packageName)
	{
		String str = instance.getApplicationContext().getPackageName();
		if(!packageName.equals(str))
		{
			 instance.finish();
	         System.exit(0);
		}
			
		
	}
	public static int StopShowAds()
	{
	//	adView.stopLoading();		
		//adView.setVisibility(View.GONE);		
		isShowAds = false;
		instance.saveGame();
		return 0;
	}
	public void saveGame()
	{
		
		SharedPreferences settings = getSharedPreferences(SAVE_REF, 0);		
		SharedPreferences.Editor editor = settings.edit();
		editor.putBoolean(SAVE_IS_ADS,isShowAds);		
		editor.commit();
	}

	public void loadGame()
	{
		
		SharedPreferences settings = getSharedPreferences(SAVE_REF, 0);	
		isShowAds = settings.getBoolean(SAVE_IS_ADS, true);
	}
	
	public static int OpenSMS(String number, String message)
	{				
		Intent smsIntent = new Intent(Intent.ACTION_VIEW);
        smsIntent.putExtra("sms_body",message); 
        smsIntent.putExtra("address", number);
        smsIntent.setType("vnd.android-dir/mms-sms");
        instance.startActivity(smsIntent);
		return 0;
	}
	
	/*
	public static int Test() {
		
			Log.v("Admob", "MRAID InApp Ad is calling..");
			UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
				@Override
				public void run() {
					
					Toast.makeText(UnityPlayer.currentActivity.getApplicationContext(), "SMS sent", 
	                         Toast.LENGTH_SHORT).show();		
				}
			});			
		Toast.makeText(mUnityPlayer.getContext(), "SMS sent", 
                         Toast.LENGTH_SHORT).show();
                         return 0;
	}
	
	//function readMessage(message){
   // guiText.text = message;
    //}  
	//Should I call sendMessage in this way?
	//PHP Code:
	 //UnityPlayer.UnitySendMessage("guiText", "readMessage", "Hello World!")  
 
	public static void sendSMSGetReceive(String phoneNumber, String message)
    {        
        String SENT = "SMS_SENT";
        String DELIVERED = "SMS_DELIVERED"; 
        PendingIntent sentPI = PendingIntent.getBroadcast(UnityPlayer.currentActivity, 0, new Intent(SENT), 0);
 
        PendingIntent deliveredPI = PendingIntent.getBroadcast(UnityPlayer.currentActivity, 0,new Intent(DELIVERED), 0);
 
        //---when the SMS has been sent---
        UnityPlayer.currentActivity.registerReceiver(new BroadcastReceiver(){        

			@Override
			public void onReceive(Context arg0, Intent arg1) {
				switch (getResultCode())
                {
                    case Activity.RESULT_OK:                    
                     //   Toast.makeText(getBaseContext(), "SMS sent", Toast.LENGTH_SHORT).show();
                        break;
                    case SmsManager.RESULT_ERROR_GENERIC_FAILURE:
                      //  Toast.makeText(getBaseContext(), "Generic failure",   Toast.LENGTH_SHORT).show();
                        break;
                    case SmsManager.RESULT_ERROR_NO_SERVICE:
                      //  Toast.makeText(getBaseContext(), "No service", Toast.LENGTH_SHORT).show();
                        break;
                    case SmsManager.RESULT_ERROR_NULL_PDU:
                      //  Toast.makeText(getBaseContext(), "Null PDU", Toast.LENGTH_SHORT).show();
                        break;
                    case SmsManager.RESULT_ERROR_RADIO_OFF:
                       // Toast.makeText(getBaseContext(), "Radio off", Toast.LENGTH_SHORT).show();
                        break;
                }
				
			}
        }, new IntentFilter(SENT));
 
        //---when the SMS has been delivered---
        UnityPlayer.currentActivity.registerReceiver(new BroadcastReceiver(){
            @Override
            public void onReceive(Context arg0, Intent arg1) {
                switch (getResultCode())
                {
                    case Activity.RESULT_OK:
                       // Toast.makeText(getBaseContext(), "SMS delivered", Toast.LENGTH_SHORT).show();
                        break;
                    case Activity.RESULT_CANCELED:
                        //Toast.makeText(getBaseContext(), "SMS not delivered",  Toast.LENGTH_SHORT).show();
                        break;                        
                }
            }
        }, new IntentFilter(DELIVERED));        
 
        SmsManager sms = SmsManager.getDefault();
        sms.sendTextMessage(phoneNumber, null, message, sentPI, deliveredPI);        
    }
	*/
	@Override protected void onDestroy ()
	{
		   if (adView != null) {
			      adView.destroy();
			    }
		mUnityPlayer.quit();
		super.onDestroy();
	}

	// Pause Unity
	@Override protected void onPause()
	{
		  if (adView != null) {
		      adView.pause();
		    }
		super.onPause();
		mUnityPlayer.pause();
	}

	// Resume Unity
	@Override protected void onResume()
	{
		super.onResume();
		  if (adView != null) {
		      adView.resume();
		    }
		mUnityPlayer.resume();
	}

	// This ensures the layout will be correct.
	@Override public void onConfigurationChanged(Configuration newConfig)
	{
		super.onConfigurationChanged(newConfig);
		mUnityPlayer.configurationChanged(newConfig);
	}

	// Notify Unity of the focus change.
	@Override public void onWindowFocusChanged(boolean hasFocus)
	{
		super.onWindowFocusChanged(hasFocus);
		mUnityPlayer.windowFocusChanged(hasFocus);
		if(!getApplicationContext().getPackageName().equals("com.hcg.baucua.tomcacop"))
		{	
			finish();
        	System.exit(0);
		}
	}
	// For some reason the multiple keyevent type is not supported by the ndk.
	// Force event injection by overriding dispatchKeyEvent().
	@Override public boolean dispatchKeyEvent(KeyEvent event)
	{
		if (event.getAction() == KeyEvent.ACTION_MULTIPLE)
			return mUnityPlayer.injectEvent(event);
		return super.dispatchKeyEvent(event);
	}

	// Pass any events not handled by (unfocused) views straight to UnityPlayer
	@Override public boolean onKeyUp(int keyCode, KeyEvent event)     { return mUnityPlayer.injectEvent(event); }
	@Override public boolean onKeyDown(int keyCode, KeyEvent event)   { return mUnityPlayer.injectEvent(event); }
	@Override public boolean onTouchEvent(MotionEvent event)          { return mUnityPlayer.injectEvent(event); }
	/*API12*/ public boolean onGenericMotionEvent(MotionEvent event)  { return mUnityPlayer.injectEvent(event); }
}
