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

            #region Find Unity
            // Find Regular Unity
            if (PropertyService.Get<string>("Unity.Path") == null || PropertyService.Get<string>("Unity.Path").Length == 1)
            {
                // Default Mac Install
                if(FileService.IsValidPath("/Applications/Unity/Unity.app/"))
                {
                     PropertyService.Set("Unity.Path", "/Applications/Unity/");
                }

            }

            // Find IPhone Unity
            if (PropertyService.Get<string>("Unity.Path.iPhone") == null || PropertyService.Get<string>("Unity.Path.iPhone").Length == 1)
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