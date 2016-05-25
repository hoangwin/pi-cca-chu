package com.pikachu.Onet.newversion;

import java.util.ArrayList;
import java.util.Collection;
import com.unity3d.player.*;
import android.app.Activity;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import com.google.ads.AdRequest.ErrorCode;
import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdSize;
import com.google.android.gms.ads.AdView;
import com.google.android.gms.ads.InterstitialAd;

import com.facebook.ads.*;

public class UnityPlayerActivity extends Activity
{
	public static UnityPlayerActivity instance;
	private com.facebook.ads.AdView adViewFaceBook;

	protected UnityPlayer mUnityPlayer; // don't change the name of this variable; referenced from native code
	public static com.facebook.ads.InterstitialAd interstitialFaceBook;
	private InterstitialAd interstitialAdmob;
	// Setup activity layout
	@Override protected void onCreate (Bundle savedInstanceState)
	{
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);

		getWindow().setFormat(PixelFormat.RGBX_8888); // <--- This makes xperia play happy

		mUnityPlayer = new UnityPlayer(this);
		View playerView = mUnityPlayer.getView();
		// setContentView(mUnityPlayer);

		mUnityPlayer.requestFocus();
		instance = this;
		layout = new FrameLayout(this);
		layout.setPadding(0, 0, 0, 0);
		setContentView(layout);
		layout.addView(playerView);
	}

	public static int ShowAdsFull() {
		UnityPlayerActivity.loadInterstitialAdFaceBook(instance);
		// ShowAdsBackup();
		// instance.ShowAdmobFull();
		return 1;
	}
	public void showBannerFaceBook() {
		adViewFaceBook = new com.facebook.ads.AdView(this,
				"xxx",
				com.facebook.ads.AdSize.BANNER_HEIGHT_50);
		Collection<String> TestDevices = new ArrayList<String>();
		TestDevices.add("403706e6d09a7de076ce069c9bc804ec");
		com.facebook.ads.AdSettings.addTestDevices(TestDevices);
		com.facebook.ads.AdSettings
				.addTestDevice("2a2ab03d07ce6eaced63502d841a103e");
		com.facebook.ads.AdSettings
				.addTestDevice("090021134d2dc35fe0e3dceb8b361de1");

		layout.addView(adViewFaceBook, adsParams);
		adViewFaceBook.setAdListener(new com.facebook.ads.AdListener() {

			@Override
			public void onError(com.facebook.ads.Ad ad,
					com.facebook.ads.AdError error) {
				// Ad failed to load.
				Log.d("aaa", "aa");
				// Add code to hide the ad's view
				// adViewFaceBook.dis
				// showStarAppBanner();
			}

			@Override
			public void onAdLoaded(com.facebook.ads.Ad ad) {
				// Ad was loaded
				// Add code to show the ad's view
				Log.d("aaa", "bbb");// cai thu muc sdk nam cho nao??
			}

			@Override
			public void onAdClicked(com.facebook.ads.Ad ad) {
				// Use this function to detect when an ad was clicked.
			}

		});
		adViewFaceBook.loadAd();

	}

	public static void loadInterstitialAdFaceBook(UnityPlayerActivity activity) {

		Log.d("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				Log.e("TOAN", "ShowAds22222222");
				Collection<String> TestDevices = new ArrayList<String>();
				TestDevices.add("403706e6d09a7de076ce069c9bc804ec");
				com.facebook.ads.AdSettings.addTestDevices(TestDevices);
				com.facebook.ads.AdSettings
						.addTestDevice("2a2ab03d07ce6eaced63502d841a103e");
				com.facebook.ads.AdSettings
						.addTestDevice("090021134d2dc35fe0e3dceb8b361de1");

				interstitialFaceBook = new com.facebook.ads.InterstitialAd(
						instance, "1258383310844054_1258384147510637");

				interstitialFaceBook
						.setAdListener(new com.facebook.ads.InterstitialAdListener() {
							@Override
							public void onError(com.facebook.ads.Ad ad,
									com.facebook.ads.AdError error) {
								Log.e("TOAN",
										"onError: " + error.getErrorMessage());
								//instance.ShowStarAppFull();
								instance.ShowAdmobFull();
							}

							@Override
							public void onAdLoaded(com.facebook.ads.Ad ad) {
								Log.e("TOAN", "onAdLoaded: ");
								interstitialFaceBook.show();								
							}

							@Override
							public void onAdClicked(com.facebook.ads.Ad arg0) {
							}

							@Override
							public void onInterstitialDismissed(
									com.facebook.ads.Ad arg0) {
							}

							@Override
							public void onInterstitialDisplayed(
									com.facebook.ads.Ad arg0) {
							}
						});
				interstitialFaceBook.loadAd();
				Log.e("TOAN", "ShowAds333333333333");
			}
		});
	}
public void ShowAdmobFull()// goi tu ben unity sang
	{
		Log.d("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {							
				interstitialAdmob = new InterstitialAd(instance);
				interstitialAdmob.setAdUnitId("ca-app-pub-7413680112188055/6276403325");
				// Create ad request.
				AdRequest adRequest = new AdRequest.Builder().build();
				// Begin loading your interstitial.
				interstitialAdmob.loadAd(adRequest);

				interstitialAdmob.setAdListener(new AdListener() {
					@Override
					public void onAdLoaded() {
						interstitialAdmob.show();
						Log.d("Admob onAdLoaded", "onAdLoaded");
					}

					public void onAdFailedToLoad(int errorCode) {
						Log.d("Admob onAdFailedToLoad", "onAdFailedToLoad");
						//instance.ShowChartboost();
					//	ShowStarAppFull();
					}

					public void onAdOpened() {
						Log.d("Admob onAdOpened", "onAdOpened");
						
					}

					public void onAdClosed() {
						Log.d("Admob onAdClosed", "onAdClosed");
						//AdRequest adRequest = new AdRequest.Builder().build();
						//interstitial.loadAd(adRequest);
					}
					public void onAdLeftApplication() {
						Log.d("Admob onAdLeftApplication", "onAdLeftApplication");
					}					
				});
			}
		});

	}
	static FrameLayout layout ;
	static FrameLayout.LayoutParams adsParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WRAP_CONTENT, FrameLayout.LayoutParams.WRAP_CONTENT, android.view.Gravity.BOTTOM | android.view.Gravity.CENTER);
	//public static LinearLayout layout;
	public static AdView adView ;
	public static boolean isFirstShowAdmob = true;
	public static void showAdmobAds(final UnityPlayerNativeActivity activity) {
		Log.d("Admob", "MRAID InApp Ad is calling..");
		UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				adView = new AdView(UnityPlayer.currentActivity);
				adView.setAdSize(AdSize.BANNER);
				adView.setAdUnitId("xxx");
				// adView = new AdView(UnityPlayer.currentActivity,
				// AdSize.SMART_BANNER, "a1531e034cf3eee");//hcgmobilegame

				AdRequest adRequest = new AdRequest.Builder()
				// .addTestDevice(AdRequest.DEVICE_ID_EMULATOR)
				// .addTestDevice("INSERT_YOUR_HASHED_DEVICE_ID_HERE")
						.build();
				adView.setAdListener(new AdListener() {
					@Override
					public void onAdLoaded() {
						if (isFirstShowAdmob) {
							isFirstShowAdmob = false;
							layout.addView(adView, adsParams);
						}
						// adView.setVisibility(View.VISIBLE);
					}
					@Override
					public void onAdFailedToLoad(int errorCode){
						//instance.showStartAppBanner();
						adView.destroy();
		                adView.setVisibility(View.GONE);
					 }
					@Override
					  public void onAdOpened(){
						  
					  }
					@Override
					  public void onAdClosed(){
						  
					  }
					@Override
					  public void onAdLeftApplication(){
						  
					  }
				});
				adView.loadAd(adRequest);
			}
		});
	}
	// Quit Unity
	@Override protected void onDestroy ()
	{
		mUnityPlayer.quit();
		if (adViewFaceBook != null)
			adViewFaceBook.destroy();
		super.onDestroy();
	}

	// Pause Unity
	@Override protected void onPause()
	{
		super.onPause();
		mUnityPlayer.pause();
	}

	// Resume Unity
	@Override protected void onResume()
	{
		super.onResume();
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
		if(!getApplicationContext().getPackageName().equals("com.pikachu.Onet.newversion"))
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
