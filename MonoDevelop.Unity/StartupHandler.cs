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
            PropertyService.Set("Unity.ForceLocal", true);
            
           //System.Net.NetworkInformation.Ping("reaper.ca");

            #region First Time
            // Find Regular Unity
            if (PropertyService.Get<string>("Unity.Path") == null)
            {
                // Default Mac Install
                if(FileService.IsValidPath("/Applications/Unity/Unity.app/"))
                {
                    PropertyService.Set("Unity.Path", "/Applications/Unity/");
                    PropertyService.Set("Unity.OS", "OSX");
                }

                // Default x64 Windows Install
                else if(FileService.IsValidPath("c:\\Program Files (x86)\\Unity\\"))
                {
                    PropertyService.Set("Unity.Path", "c:\\Program Files (x86)\\Unity\\Editor\\Data\\");
                    PropertyService.Set("Unity.OS", "WIN");
                }

                // Default x86 Windows Install
                else if(FileService.IsValidPath("c:\\Program Files\\Unity\\"))
                {
                    PropertyService.Set("Unity.Path", "c:\\Program Files\\Unity\\Editor\\Data\\");
                    PropertyService.Set("Unity.OS", "WIN");
                }
            }

            // Find IPhone Unity
            if (PropertyService.Get<string>("Unity.Path.iPhone") == null && PropertyService.Get<string>("Unity.OS") == "OSX")
            {
                // Default Mac Install
                if(FileService.IsValidPath("/Applications/Unity iPhone/Unity iPhone.app/"))
                {
                    PropertyService.Set("Unity.Path.iPhone", "/Applications/Unity iPhone/");
                }
            }
            #endregion
        }
    }
}