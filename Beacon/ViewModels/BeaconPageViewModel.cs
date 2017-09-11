using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Beacon.Models;
using Estimotes;
using Xamarin.Forms;

namespace Beacon.ViewModels
{
    public class BeaconPageViewModel : BaseViewModel
    {
        public ObservableCollection<IBeacon> BeaconsDetected 
        {
            get; set;
        }

        public BeaconPageViewModel()
        {
            Init();
            SubscribeMessages();
        }

        void Init()
        {
            BeaconsDetected = new ObservableCollection<IBeacon>();
        }

        void SubscribeMessages()
        {
            MessagingCenter.Subscribe<object, List<IBeacon>>(this as object, "UpdateCollection", (sender, args) =>
            {
                //TODO cualquier operacion adicional antes de el callback o definir tu propio callback aqui
                OnBeaconDetected(sender, args);
            });
        }

        public void OnBeaconDetected(object sender, List<IBeacon> beacons)
        {
            var toRemove = new List<IBeacon>();
            foreach (var beacon in BeaconsDetected)
            {
                if (!beacons.Any(x => x == beacon))
                    toRemove.Add(beacon);
            }

            foreach (var beacon in beacons)
            {
                if (!BeaconsDetected.Any(x => x == beacon))
                    BeaconsDetected.Add(beacon);
            }

            foreach (var beacon in toRemove)
            {
                BeaconsDetected.Remove(beacon);
            }
        }
    }
}
