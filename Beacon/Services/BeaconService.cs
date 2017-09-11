using System;
using System.Collections.Generic;
using Estimotes;
using System.Diagnostics;
using Xamarin.Forms;
using Beacon.Models;
using System.Linq;

namespace Beacon.Services
{
    public static class BeaconService
    {
		const string RANGED = "ranged";
		const string MONITORING = "monitoring";
        public static DateTime LastTimeRefreshed;

        public static IList<BeaconRegion> Regions => new List<BeaconRegion>
        {
            new BeaconRegion("YoiRegion", "f7826da6-4fa2-4e98-8024-bc5b71e0893e")
        };

        public static void OnBeaconManagerInit(BeaconInitStatus status)
        {
            Debug.WriteLine($"Yoi - Estimote Manager Status: {status}");
            if (status != BeaconInitStatus.Success)
                return;

            EstimoteManager.Instance.Ranged += OnRanged;
            foreach (var region in Regions)
            {
                EstimoteManager.Instance.StartRanging(region);
            }

            EstimoteManager.Instance.RegionStatusChanged += OnBeaconRegionStatusChanged;
            EstimoteManager.Instance.StopAllMonitoring();
            foreach (var region in Regions)
            {
                EstimoteManager.Instance.StartMonitoring(region);
            }
        }

        static void OnRanged(object sender, IEnumerable<IBeacon> beacons)
        {
            OnBeaconsDetected(sender, beacons);
        }

        async static void OnBeaconRegionStatusChanged(object sender, BeaconRegionStatusChangedEventArgs args)
        {
            var beacons = await EstimoteManager.Instance.FetchNearbyBeacons(args.Region);
            OnBeaconsDetected(sender, beacons);
        }

        static void OnBeaconsDetected(object sender, IEnumerable<IBeacon> beacons)
        {
			if (LastTimeRefreshed.AddSeconds(20) >= DateTime.Now)
				return;
			LastTimeRefreshed = DateTime.Now;
            var beaconsDetected = new List<IBeacon>();
            foreach (var beacon in beacons)
            {
                Debug.WriteLine($"Beacon detected with Proximity {beacon.Uuid}, Major {beacon.Major}, Minor {beacon.Minor}");
                beaconsDetected.Add(beacon);
            }
            MessagingCenter.Send(sender, "UpdateCollection", beaconsDetected);
        }
    }
}
