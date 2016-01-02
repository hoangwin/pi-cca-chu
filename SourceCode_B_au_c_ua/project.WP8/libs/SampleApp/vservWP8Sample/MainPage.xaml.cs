using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using vservWP8Sample.Resources;
using vservWindowsPhone;
namespace vservWP8Sample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        // Constructor
        //Initialize VservSDK
        VservAdControl VAC = VservAdControl.Instance;
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += AppView_Loaded; //Event for Showing Ad on Start
            VAC.SetRequestTimeOut(30);
            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();

            VAC.VservAdClosed += new EventHandler(VACCallback_OnVservAdClosing);
            VAC.VservAdError += new EventHandler(VACCallback_OnVservAdNetworkError);
            VAC.VservAdNoFill += new EventHandler(VACCallback_OnVservAdNoFill);
        }
        void VACCallback_OnVservAdClosing(object sender, EventArgs e)
        {
            MessageBox.Show("Ad Closed by user", "Interstitial Ad", MessageBoxButton.OKCancel);
            BuildLocalizedApplicationBar();
            
        }
        void VACCallback_OnVservAdNetworkError(object sender, EventArgs e)
        {
            MessageBox.Show("Data connection not available", "No Data", MessageBoxButton.OKCancel);
        }

        void VACCallback_OnVservAdNoFill(object sender, EventArgs e)
        {
            MessageBox.Show("No Ad Available", "No Fill", MessageBoxButton.OKCancel);
            if (adGrid != null)
                adGrid.Visibility = Visibility.Collapsed;
        }
        private void Render_Ad(object sender, RoutedEventArgs e)
        {
            if (adGrid != null)
                adGrid.Visibility = Visibility.Visible;
            VAC.RenderAd("e9b1dfc8"/*Banner Zone Id*/, adGrid/* Grid object on which the banner Ad will be displayed*/);
            return;
        }
        private void AppView_Loaded(object sender, RoutedEventArgs e)
        {
     //       VAC.DisplayAd("8063"/* Zone Id*/, LayoutRoot/* Layout over which the Ad will be displayed*/);
            if (adGrid != null)
                adGrid.Visibility = Visibility.Visible;
            VAC.RenderAd("e9b1dfc8", adGrid);
        }
        private void Display_Ad(object sender, RoutedEventArgs e)
        {
       //     BuildLocalizedApplicationBar();
            //// This Method is called for showing Interstitial Ad
            try
            {
                VAC.DisplayAd("e9b1dfc8"/* Zone Id*/, LayoutRoot/* Layout over which the Ad will be displayed*/);
            }
            catch
            {
            }
            //Thickness sz = LayoutRoot.Margin;
            
            return;
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }
        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            appBarButton.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }
    }
}