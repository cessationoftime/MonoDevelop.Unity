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
            // Find Regular Unity
            if (PropertyService.Get<string>("Unity.Base.Documentation.Path") == null)
            {
                // Default Mac Install
                if(FileService.IsValidPath("/Applications/Unity/Unity.app/"))
                {
                    PropertyService.Set("Unity.Base.Documentation.Path", "/Applications/Unity/");
                    PropertyService.Set("Unity.OS", "OSX");
                }

                // Default x64 Windows Install
                else if(FileService.IsValidPath("c:\\Program Files (x86)\\Unity\\"))
                {
                    PropertyService.Set("Unity.Base.Documentation.Path", "c:\\Program Files (x86)\\Unity\\Editor\\Data\\");
                    PropertyService.Set("Unity.OS", "WIN");
                }

                // Default x86 Windows Install
                else if(FileService.IsValidPath("c:\\Program Files\\Unity\\"))
                {
                    PropertyService.Set("Unity.Base.Documentation.Path", "c:\\Program Files\\Unity\\Editor\\Data\\");
                    PropertyService.Set("Unity.OS", "WIN");
                }
            }

            // Find IPhone Unity
            if (PropertyService.Get<string>("Unity.iPhone.Documentation.Path") == null && PropertyService.Get<string>("Unity.OS") == "OSX")
            {
                // Default Mac Install
                if(FileService.IsValidPath("/Applications/Unity iPhone/Unity iPhone.app/"))
                {
                    PropertyService.Set("Unity.iPhone.Documentation.Path", "/Applications/Unity iPhone/");
                }
            }
            #endregion
        }
    }
}