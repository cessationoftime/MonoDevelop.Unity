using System;
using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Components.Commands;

namespace MonoDevelop.Unity
{
    public class StartupHandler : CommandHandler
    {
        protected override void Run()
        {
            // Internet Connection
            PropertyService.Set("Unity.Connection", true);

           //System.Net.NetworkInformation.Ping("reaper.ca");

            #region First Time

            if (PropertyService.Get<string>("Unity.Base.Path", null) == null)
            {
                Helpers.FindUnity();
            }

            // Find IPhone Unity
            if (PropertyService.Get<string>("Unity.iPhone.Path", null) == null && PropertyService.Get<string>("Unity.OS", null) == "OSX")
            {
                Helpers.FindUnityiPhone();
            }
            #endregion
        }
    }
}