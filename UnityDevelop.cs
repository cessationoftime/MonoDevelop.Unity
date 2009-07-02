using System;
using System.Collections.Generic;
using System.Text;


using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;

namespace UnityDevelop
{

    public class UnityHandler
    {
        private bool openInBrowser = true;

        #region Overloaded Open

        public bool Open(Unity.Documentation target)
        {
            if ( this.openInBrowser )
            {
                switch (target)
                {
                    case Unity.Documentation.Manual:
                        return this.Open(Unity.REMOTE_VIEW_MANUAL_URI);
                    case Unity.Documentation.Reference:
                        return this.Open(Unity.REMOTE_VIEW_REFERENCE_URI);
                    case Unity.Documentation.ScriptReference:
                        return this.Open(Unity.REMOTE_VIEW_SCRIPT_REFERENCE_URI);
                }
            }
            return false;
        }

        public bool Open(Unity.Documentation target, string query)
        {
            // Searching
            if (query != null && query.Length > 0)
            {
                switch (target)
                {
                    case Unity.Documentation.Manual:
                        return this.Open(Unity.REMOTE_SEARCH_MANUAL_URI + System.Web.HttpUtility.UrlEncode(query));
                    case Unity.Documentation.Reference:
                        return this.Open(Unity.REMOTE_SEARCH_REFERENCE_URI + System.Web.HttpUtility.UrlEncode(query));
                    case Unity.Documentation.ScriptReference:
                        return this.Open(Unity.REMOTE_SEARCH_SCRIPT_REFERENCE_URI + System.Web.HttpUtility.UrlEncode(query));
                }
            }
            return false;
        }

        public bool Open(string address)
        {
            /*
            IFileService fileService = (IFileService)MonoDevelop.Core.Services.ServiceManager.GetService(typeof(IFileService));
            fileService.OpenFile(search_url);
            */
            //IdeApp.Services.PlatformService
            if ( this.openInBrowser )
            {
              //  FileService.
                //Runtime.ProcessService.StartProcess(
                IdeApp.Workbench.OpenDocument(address);
//                IdeApp.OpenFiles(address);
                //System.Diagnostics.Process.Start(address);
                return true;
            }
            return false;
        }

        #endregion
    }

}