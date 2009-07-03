

using System;
using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Components.Commands;


namespace UnityDevelop
{
    public class FindUnityOnStartupHandler : CommandHandler
    {
        protected override void Run()
        {
            // Find Regular Unity
            if (PropertyService.Get<string>("UnityDevelop.UnityPath") == null || PropertyService.Get<string>("UnityDevelop.UnityPath").Length == 1)
            {
                // Default Mac Install
                if(FileService.IsValidPath("/Applications/Unity/Unity.app/"))
                {
                     PropertyService.Set("UnityDevelop.UnityPath", "/Applications/Unity/");
                }

            }

            // Find IPhone Unity
            if (PropertyService.Get<string>("UnityDevelop.UnityIPhonePath") == null || PropertyService.Get<string>("UnityDevelop.UnityIPhonePath").Length == 1)
            {
                // Default Mac Install
                if(FileService.IsValidPath("/Applications/Unity iPhone/Unity iPhone.app/"))
                {
                     PropertyService.Set("UnityDevelop.UnityIPhonePath", "/Applications/Unity iPhone/");
                }
            }


        }
    }
    public class CheckInternetConnectionOnStartupHandler : CommandHandler
    {
        protected override void Run()
        {
            PropertyService.Set("UnityDevelop.ForceLocal", true);
        }
    }
}
