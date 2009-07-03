using System;
using System.Collections.Generic;
using System.Text;


using MonoDevelop.Core;
using MonoDevelop.Core.Gui;

namespace UnityDevelop
{

    public class UnityHandler
    {

        #region Overloaded Open

        public bool Open(Unity.Documentation target)
        {
            // Currently Mono does not have System.Net.NetworkInformation.Ping Implemented so we have to assume connection
            if (
                PropertyService.Get<bool>("UnityDevelop.ForceLocal", false) ||
                PropertyService.Get<bool>("UnityDevelop.Connection", true) )
            {
                switch (target)
                {
                    case Unity.Documentation.Manual:
                        return this.Open("file://" + PropertyService.Get<string>("UnityDevelop.UnityPath") +
                            Unity.LOCAL_VIEW_MANUAL_URI.Replace('|', System.IO.Path.DirectorySeparatorChar));
                    case Unity.Documentation.Reference:
                        return this.Open("file://" + PropertyService.Get<string>("UnityDevelop.UnityPath") +
                            Unity.LOCAL_VIEW_REFERENCE_URI.Replace('|', System.IO.Path.DirectorySeparatorChar));
                    case Unity.Documentation.ScriptReference:
                        return this.Open("file://" + PropertyService.Get<string>("UnityDevelop.UnityPath") +
                            Unity.LOCAL_SEARCH_SCRIPT_REFERENCE_URI.Replace('|', System.IO.Path.DirectorySeparatorChar));
                }

            }
            else
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
                // Currently Mono does not have System.Net.NetworkInformation.Ping Implemented so we have to assume connection
                if (
                    PropertyService.Get<bool>("UnityDevelop.ForceLocal", false) ||
                    PropertyService.Get<bool>("UnityDevelop.Connection", true) )
                {
                    switch (target)
                    {
                        case Unity.Documentation.ScriptReference:
                            return this.Open("file://" + PropertyService.Get<string>("UnityDevelop.UnityPath") +
                                         Unity.LOCAL_SEARCH_SCRIPT_REFERENCE_URI.Replace('|', System.IO.Path.DirectorySeparatorChar) +
                                         System.Web.HttpUtility.UrlEncode(query));
                    }

                }
                else
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
            }
            return false;
        }

        public bool Open(string address)
        {
            if ( PropertyService.Get<bool>("UnityDevelop.OpenInBrowser", true))
            {
            //    System.Diagnostics.Process.Start(address);
//                MessageService.ShowMessage(address);
                Services.PlatformService.ShowUrl(address);
                return true;
            }
            return false;
        }

        #endregion
    }

}