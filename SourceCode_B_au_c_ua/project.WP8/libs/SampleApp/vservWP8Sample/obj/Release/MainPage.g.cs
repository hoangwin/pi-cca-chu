﻿#pragma checksum "C:\Users\ashay.PC053\documents\visual studio 2012\Projects\vservWP8Sample\vservWP8Sample\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6B7339565181CD497536836D5DB44B1D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace vservWP8Sample {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button ShowButton;
        
        internal System.Windows.Controls.Button RenderButton;
        
        internal System.Windows.Controls.Grid adGrid;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/vservWP8Sample;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ShowButton = ((System.Windows.Controls.Button)(this.FindName("ShowButton")));
            this.RenderButton = ((System.Windows.Controls.Button)(this.FindName("RenderButton")));
            this.adGrid = ((System.Windows.Controls.Grid)(this.FindName("adGrid")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
        }
    }
}

