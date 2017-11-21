﻿using System;
using Foundation;
using MasterQ.Helpers;
using MasterQ.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(implementorType: typeof(iOSNotification))]
namespace MasterQ.iOS
{
    public class iOSNotification : IFNotification
    {
        public iOSNotification()
        {
        }

        public void SendNotification(string act, string body)
        {
            UILocalNotification notification = new UILocalNotification();
            notification.ApplicationIconBadgeNumber = 1;
            NSDate.FromTimeIntervalSinceNow(15);
            //notification.AlertTitle = "Alert Title"; // required for Apple Watch notifications
            notification.AlertAction = act;
            notification.AlertBody = body;
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);   
        }
    }
}