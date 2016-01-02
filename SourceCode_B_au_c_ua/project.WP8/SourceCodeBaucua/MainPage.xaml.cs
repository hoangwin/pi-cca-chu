using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Windows.Foundation;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Tasks;
using UnityApp = UnityPlayer.UnityApp;
using UnityBridge = WinRTBridge.WinRTBridge;
using InMobi.WP.AdSDK;
using System.IO;
namespace SourceCodeBaucua
{
	public partial class MainPage : PhoneApplicationPage
	{
		private bool _unityStartedLoading;
		private bool _useLocation;

        public static int isShowAds = 1;//1 = true
		// Constructor
      
		public MainPage()
		{
			var bridge = new UnityBridge();
			UnityApp.SetBridge(bridge);
			InitializeComponent();
			bridge.Control = DrawingSurfaceBackground;
            WP8Statics.WP8FunctionHandle += WP8Statics_OpenSMSHandle;
            WP8Statics.WP8FunctionHandle1 += WP8Statics_StopAds;
            loadGame();
		}
        void WP8Statics_OpenSMSHandle(object sender, EventArgs e)
        {
            String str = (string)sender;            
            string[] strs = str.Split('|');

            SmsComposeTask smsComposeTask = new SmsComposeTask();
            smsComposeTask.To = strs[0];
            smsComposeTask.Body = strs[1];

            smsComposeTask.Show();
        }
        void WP8Statics_StopAds(object sender, EventArgs e)
        {
            isShowAds = 0;
            saveGame();
        }
		private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
		{
			if (!_unityStartedLoading)
			{
				_unityStartedLoading = true;

				UnityApp.SetLoadedCallback(() => { Dispatcher.BeginInvoke(Unity_Loaded); });

				var content = Application.Current.Host.Content;
				var width = (int)Math.Floor(content.ActualWidth * content.ScaleFactor / 100.0 + 0.5);
				var height = (int)Math.Floor(content.ActualHeight * content.ScaleFactor / 100.0 + 0.5);

				UnityApp.SetNativeResolution(width, height);
				UnityApp.SetRenderResolution(width, height);
				UnityPlayer.UnityApp.SetOrientation((int)Orientation);

				DrawingSurfaceBackground.SetBackgroundContentProvider(UnityApp.GetBackgroundContentProvider());
				DrawingSurfaceBackground.SetBackgroundManipulationHandler(UnityApp.GetManipulationHandler());
              //  CreateAd(DrawingSurfaceBackground);
                if (isShowAds == 1)
                    AdsManager.showAds(DrawingSurfaceBackground, AdsManager.INDEX_INMOBI);
			}
		}

		private void Unity_Loaded()
		{
			SetupGeolocator();
		}

		private void PhoneApplicationPage_BackKeyPress(object sender, CancelEventArgs e)
		{
			e.Cancel = UnityApp.BackButtonPressed();
		}

		private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
		{
			UnityApp.SetOrientation((int)e.Orientation);
		}

		protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!UnityApp.IsLocationEnabled())
                return;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
                _useLocation = (bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"];
            else
            {
                MessageBoxResult result = MessageBox.Show("Can this application use your location?",
                    "Location Services", MessageBoxButton.OKCancel);
                _useLocation = result == MessageBoxResult.OK;
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = _useLocation;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

		private void SetupGeolocator()
        {
            if (!_useLocation)
                return;

            try
            {
				UnityApp.EnableLocationService(true);
                Geolocator geolocator = new Geolocator();
				geolocator.ReportInterval = 5000;
                IAsyncOperation<Geoposition> op = geolocator.GetGeopositionAsync();
                op.Completed += (asyncInfo, asyncStatus) =>
                    {
                        if (asyncStatus == AsyncStatus.Completed)
                        {
                            Geoposition geoposition = asyncInfo.GetResults();
                            UnityApp.SetupGeolocator(geolocator, geoposition);
                        }
                        else
                            UnityApp.SetupGeolocator(null, null);
                    };
            }
            catch (Exception)
            {
                UnityApp.SetupGeolocator(null, null);
            }
        }

        public void saveGame()
        {
            //Obtain a virtual store for application
            IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();

            //Create new subdirectory
            fileStorage.CreateDirectory("save");
            //Create a new StreamWriter, to write the file to the specified location.
            StreamWriter fileWriter = new StreamWriter(new IsolatedStorageFileStream("save\\save.txt", FileMode.OpenOrCreate, fileStorage));
            //Write the contents of our TextBox to the file.
            fileWriter.WriteLine(isShowAds);
            //Close the StreamWriter.
            fileWriter.Close();
        }
      
        public void loadGame()
        {
            //Obtain a virtual store for application
            IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
            //Create a new StreamReader
            StreamReader fileReader = null;
            try
            {
                //Read the file from the specified location.
                fileReader = new StreamReader(new IsolatedStorageFileStream("save\\save.txt", FileMode.Open, fileStorage));
                //Read the contents of the file (the only line we created).
                string textFile = fileReader.ReadLine();
                //Write the contents of the file to the TextBlock on the page.
                fileReader.Close();
                isShowAds = int.Parse(textFile);
                return;
            }
            catch
            {
                //If they click the view button first, we need to handle the fact that the file hasn't been created yet.

            }
            isShowAds = 1;
           
        }

        //ads INMOBI
        /*
        private void CreateAd(DrawingSurfaceBackgroundGrid stackContainer)
        {
            SDKUtility.LogLevel = LogLevels.IMLogLevelDebug;

            IMAdView AdView = new IMAdView();

            AdView.AdSize = IMAdView.INMOBI_AD_UNIT_320X50;

            //Subscribe for IMAdView events
            AdView.OnAdRequestFailed += AdView_AdRequestFailed;
            AdView.OnAdRequestLoaded += AdView_AdRequestLoaded;
            AdView.OnDismissAdScreen += new EventHandler(AdView_DismissFullAdScreen);
            AdView.OnLeaveApplication += new EventHandler(AdView_LeaveApplication);
            AdView.OnShowAdScreen += new EventHandler(AdView_ShowFullAdScreen);


            //Set the AppId. Provide you AppId
            AdView.AppId = "50d61d6e24d54c82b517c226d1b43210";
            AdView.RefreshInterval = 20;
            AdView.AnimationType = IMAdAnimationType.SLIDE_IN_LEFT;
            AdView.VerticalAlignment = VerticalAlignment.Bottom;                
            IMAdRequest imAdRequest = new IMAdRequest();



            AdView.LoadNewAd(imAdRequest);

            //Add IMAdView to Container
            stackContainer.Children.Add(AdView);

        }

        void AdView_AdRequestFailed(object sender, IMAdViewErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ErrorCode.ToString() + e.ErrorDescription.ToString());
        }

        void AdView_AdRequestLoaded(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Ad request loaded.");
        }

        //Invoked when the full screen Ad has been opened
        void AdView_ShowFullAdScreen(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Displaying full screen");
        }

        //Invoked when navigating out of application as Click To Action on IMAdView 
        void AdView_LeaveApplication(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Moving out of application");
        }

        //Invoked when full screen Ad displayed is closed
        void AdView_DismissFullAdScreen(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Full screen closed");
        }
         * */
	}
}