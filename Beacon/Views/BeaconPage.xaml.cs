using System;
using System.Collections.Generic;
using Beacon.ViewModels;
using Xamarin.Forms;

namespace Beacon.Views
{
    public partial class BeaconPage : ContentPage
    {
        public BeaconPage()
        {
            BindingContext = new BeaconPageViewModel();
            InitializeComponent();
        }
    }
}
