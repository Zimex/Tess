﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Drako3
{
    public partial class NewGameSettings : PhoneApplicationPage
    {
        public NewGameSettings()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
             NavigationService.Navigate(new Uri("/PanoramaPage1.xaml", UriKind.Relative));

        }
    }
}