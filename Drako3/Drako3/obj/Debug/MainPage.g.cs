﻿#pragma checksum "C:\Users\urbanowicz\documents\visual studio 2013\Projects\Drako3\Drako3\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "20E0EDE77D3149BE6972B928CF892368"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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


namespace Drako3 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock menuTitle;
        
        internal System.Windows.Controls.Button newGameButton;
        
        internal System.Windows.Controls.Button loadGameButton;
        
        internal System.Windows.Controls.Button settingsButton;
        
        internal System.Windows.Controls.Button exitButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Drako3;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.menuTitle = ((System.Windows.Controls.TextBlock)(this.FindName("menuTitle")));
            this.newGameButton = ((System.Windows.Controls.Button)(this.FindName("newGameButton")));
            this.loadGameButton = ((System.Windows.Controls.Button)(this.FindName("loadGameButton")));
            this.settingsButton = ((System.Windows.Controls.Button)(this.FindName("settingsButton")));
            this.exitButton = ((System.Windows.Controls.Button)(this.FindName("exitButton")));
        }
    }
}

